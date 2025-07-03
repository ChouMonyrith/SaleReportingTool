using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleReportingTool
{
    public class SaleService
    {
        private readonly string _connectionString = "Server=MSI\\SQLDEVSEVER;Database=ReportSale;Integrated Security=True;TrustServerCertificate=True";

        public List<SaleDto> GetSalesData(DateTime startDate, DateTime endDate)
        {
            var sales = new List<SaleDto>();

            var query = "SELECT PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE FROM PRODUCTSALES WHERE SALEDATE BETWEEN @STARTDATE AND @ENDDATE;";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@STARTDATE", startDate.Date);
                    command.Parameters.AddWithValue("@ENDDATE", endDate.Date);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sales.Add(new SaleDto
                        {
                            ProductCode = reader["PRODUCTCODE"].ToString(),
                            ProductName = reader["PRODUCTNAME"].ToString(),
                            Quantity = (int)reader["QUANTITY"],
                            UnitPrice = (decimal)reader["UNITPRICE"],
                            SaleDate = (DateTime)reader["SALEDATE"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                string logDir = "logs";
                Directory.CreateDirectory(logDir);
                File.AppendAllText(Path.Combine(logDir, "errors.txt"), $"{DateTime.Now} - ERROR: {ex.Message}\n");
                return null; 
            }
            return sales;
        }
    }
}
