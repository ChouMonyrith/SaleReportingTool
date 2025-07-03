using DevExpress.XtraReports.Serialization;
using DevExpress.XtraReports.UI;
using SaleReportingTool.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleReportingTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

  

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var dataService = new SaleService();
            var saleData = dataService.GetSalesData(dtpStartDate.Value, dtpEndDate.Value);

            if(saleData == null)
            {
                MessageBox.Show("No sales data found for the given date range");
                return;
            }

            if (saleData.Count == 0)
            {
                MessageBox.Show("No results found for the selected date range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); // [cite: 58]
                return;
            }

            decimal grandTotal = saleData.Sum(s => s.Quantity * s.UnitPrice);
            
            var report = new XtraReport1();
            report.DataSource = saleData;

            report.SetGrandTotal(grandTotal);
            report.SetStartDate(dtpStartDate.Value);
            report.SetEndDate(dtpEndDate.Value);

            var printTool = new ReportPrintTool(report);
            report.ShowPreview();


        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {
            var saleService = new SaleService();
            var saleData = saleService.GetSalesData(dtpStartDate.Value, dtpEndDate.Value);

            if (saleData == null)
            {
                MessageBox.Show("No sales data found for the given date range");
                return;
            }

            var report = new XtraReport1();
            report.DataSource = saleData;

            report.SetGrandTotal(saleData.Sum(s => s.Quantity * s.UnitPrice));
            report.SetStartDate(dtpStartDate.Value);
            report.SetEndDate(dtpEndDate.Value);

            using(SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf|All Files|*.*";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        report.ExportToPdf(saveFileDialog.FileName);
                        MessageBox.Show("Report exported to " + saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error exporting report: " + ex.Message);
                    }
                }
            }

            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbLanguage.Items.Add(new { Name = "English", Value = "en-US" });
            cmbLanguage.Items.Add(new { Name = "Khmer", Value = "km-KH" });
            cmbLanguage.DisplayMember = "Name";
            cmbLanguage.ValueMember = "Value";
            cmbLanguage.SelectedIndex = 0;
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguage.SelectedItem != null)
            {
                var selectedCulture = (cmbLanguage.SelectedItem as dynamic).Value;
               
                switch(selectedCulture)
                {
                    case "en-US":
                        lblStartDate.Text = "Start Date";
                        lblEndDate.Text = "End Date";
                        btnGenerate.Text = "Generate";
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                        break;
                    case "km-KH":
                        label2.Text = "";
                        lblTitle.Text = "របាយការណ៍លក់";
                        lblEndDate.Text = "ថ្ថៃចាប់ផ្ដើម";
                        lblStartDate.Text = "ថ្ងៃបញ្ចប់";
                        btnGenerate.Text = "បង្កើត";
                        btnExportToPdf.Text = "ដោនឡូតជាPDF";
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("km-KH");
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("km-KH");
                        break;
                }
            }
        }

    }
}
