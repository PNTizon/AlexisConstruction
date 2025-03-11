using AlexisConstruction.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class printReceipt: Form
    {
        OrderDetails order;
        List<Orders> _list;
        public printReceipt(OrderDetails orders,List<Orders> list)
        {
            InitializeComponent();
            order = orders;
            _list = list;
        }

        private void printReceipt_Load(object sender, EventArgs e)
        {

            Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("pBookingID",order.BookingsID.ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("pFullname",order.CustomerName),
                new Microsoft.Reporting.WinForms.ReportParameter("pAddress", order.Address),
                new Microsoft.Reporting.WinForms.ReportParameter("pNumber",order.ContactNumber),
                new Microsoft.Reporting.WinForms.ReportParameter("pBillingDate",order.BillingDate.ToString("MM/dd/yyyy")),
                new Microsoft.Reporting.WinForms.ReportParameter("pTotal",order.ReceiptOrder.Sum(o => o.TotalAmount).ToString("N2")),
                new Microsoft.Reporting.WinForms.ReportParameter("pPaymentMethod",order.MOP)
            };

            this.reportViewer1.LocalReport.SetParameters(p);
            Microsoft.Reporting.WinForms.ReportDataSource dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet6", _list);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
