# C# WinForms Product Sales Reporter

This project is a submission for the C# Developer Internship Coding Test. It is a Windows Forms application that connects to a SQL Server database to fetch, filter, and display product sales data using a DevExpress XtraReport.

## Features

- **Date Range Filtering:** Users can select a start and end date to filter the sales data.
- **Grouped Reporting:** The report groups sales by `ProductCode` and displays the `ProductName`.
- **Summaries and Totals:** The report calculates and displays total quantity and revenue for each product group, as well as a grand total for the entire report.
- **Error Logging:** SQL connection and query errors are logged to a local `logs/errors.txt` file.
- **Dynamic PDF Export:** Users can export the generated report to a PDF file.
- **Multilingual Support:** The UI and report headers can be switched between English and Khmer.

![Application Main Screen](Screenshot%202025-07-03%20112902.png)
![Report Preview](Screenshot%202025-07-03%20112919.png)

## How to Clone and Set Up

### 1. Clone the Repository

Open a terminal or command prompt and run:

```bash
git clone https://github.com/ChouMonyrith/SaleReportingTool.git
cd SaleReportingTool
```

### 2. Database Setup

- Open SQL Server Management Studio (or your preferred SQL tool).
- Run the `setup.sql` script included in this repository. This will create the `PRODUCTSALES` table and insert the required sample data.

**Sample Table Creation & Data:**
```sql
CREATE TABLE PRODUCTSALES (
    SALEID INT PRIMARY KEY,
    PRODUCTCODE NVARCHAR(20),
    PRODUCTNAME NVARCHAR(100),
    QUANTITY INT,
    UNITPRICE DECIMAL(18, 2),
    SALEDATE DATE
);
GO

-- Insert sample data into the table
INSERT INTO PRODUCTSALES (SALEID, PRODUCTCODE, PRODUCTNAME, QUANTITY, UNITPRICE, SALEDATE)
VALUES
(1, 'P001', 'Pen', 10, 1.50, '2025-06-20'),
(2, 'P001', 'Pen', 5, 1.50, '2025-06-25'),
(3, 'P002', 'Notebook', 3, 3.20, '2025-06-21'),
(4, 'P003', 'Eraser', 15, 0.80, '2025-06-22'),
(5, 'P002', 'Notebook', 7, 3.20, '2025-06-28'),
(6, 'P001', 'Pen', 12, 1.55, '2025-07-01');
GO

PRINT 'Table PRODUCTSALES created and seeded successfully.';
GO
```

### 3. Configure the Connection String

- Open the solution in Visual Studio.
- Navigate to the `SalesDataService.cs` file.
- On line 12, update the `_connectionString` variable with your SQL Server connection details, for example:

```csharp
private readonly string _connectionString = "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;";
```

### 4. Build and Run

- Build the solution in Visual Studio.
- Run the application (press `F5`).

## Dependencies

- .NET Framework 4.7.2 (or later)
- DevExpress WinForms Components v24.1

## Notes

- All errors related to SQL connections or queries will be logged in the `logs/errors.txt` file.
- For multilingual support, use the language toggle option within the application's UI.

## License

This project is provided for demonstration and testing purposes only. See the [LICENSE](LICENSE) file for more details.
