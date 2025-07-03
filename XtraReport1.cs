using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace SaleReportingTool
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
 
        public XtraReport1()
        {
            InitializeComponent();
        }

        public void SetGrandTotal(decimal totalValue)
        {
            // This sets the text of the label you named in the designer
            lblGrandTotal.Text = totalValue.ToString(); // The :C formats it as currency
        }

        public void SetStartDate(DateTime startDate)
        {
            // This sets the text of the label you named in the designer
            lblStartDate.Text = startDate.ToString(); // The :C formats it as currency
        }

        public void SetEndDate(DateTime endDate)
        {
            // This sets the text of the label you named in the designer
            lblEndDate.Text = endDate.ToString(); // The :C formats it as currency
        }

    }
}
