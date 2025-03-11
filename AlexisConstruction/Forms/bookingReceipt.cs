using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Reporting.WinForms;
//using Microsoft.Reporting.WebForms;

namespace AlexisConstruction.Forms
{
    public partial class bookingReceipt : Form
    {
        BookingDetails _booking;
        List<Orders> _orders;
        public bookingReceipt(BookingDetails details, List<Orders> list)
        {
            InitializeComponent();
            _booking = details;
        }

        private void bookingReceipt_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("RECEIPT", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookedDate", BookingDetails.BookedDate);
                    cmd.Parameters.AddWithValue("@clientID", _booking.BookingsID);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // First, set the report path
                    this.reportViewer1.LocalReport.ReportPath = "BookingReceipt.rdlc";

                    // Clear existing data sources
                    this.reportViewer1.LocalReport.DataSources.Clear();

                    // Add new data source(s)
                    ReportDataSource reportDataSource = new ReportDataSource("DataSet2", dt);
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                    ReportParameter name = new ReportParameter("fullname", _booking.CustomerName);
                    ReportParameter bookingDate = new ReportParameter("billingDate", _booking.BillingDate.ToString("MM/dd/yyyy"));
                    ReportParameter bookedDate = new ReportParameter("bookedDate", BookingDetails.BookedDate.ToString("MM/dd/yyyy"));
                    ReportParameter total = new ReportParameter("totalamount", (_booking.BookingReceipt.Sum(r => r.TotalAmount)).ToString("N2"));
                    ReportParameter paymentMethod = new ReportParameter("paymentMethod", _booking.MOP);

                    reportViewer1.LocalReport.SetParameters(name);
                    reportViewer1.LocalReport.SetParameters(bookedDate);
                    reportViewer1.LocalReport.SetParameters(bookingDate);
                    reportViewer1.LocalReport.SetParameters(total);
                    reportViewer1.LocalReport.SetParameters(paymentMethod);

                    
                    this.reportViewer1.RefreshReport();
                }
            }

            //this.reportViewer1.LocalReport.DataSources.Add(
            //    new Microsoft.Reporting.WinForms.ReportDataSource("DateSet2", dt));

            // Create and set report parameters

            //// Now set parameters after the report path is specified
            //this.reportViewer1.LocalReport.SetParameters(p);

            // Finally, refresh the report


            //    Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            //    {
            //new Microsoft.Reporting.WinForms.ReportParameter("fullName", _booking.CustomerName),
            //new Microsoft.Reporting.WinForms.ReportParameter("billingDate", _booking.BillingDate.ToString("MM/dd/yyyy")),
            //new Microsoft.Reporting.WinForms.ReportParameter("bookedDate", _booking.BookedDate.ToString("MM/dd/yyyy")),
            //new Microsoft.Reporting.WinForms.ReportParameter("totalAmount", _booking.BookingReceipt.Sum(o => o.TotalAmount).ToString("N2")),
            //new Microsoft.Reporting.WinForms.ReportParameter("paymentMethod", _booking.MOP)
            //    };
            //    this.reportViewer1.LocalReport.ReportPath = "BookingReceipt.rdlc";

            //    this.reportViewer1.LocalReport.SetParameters(p);


            //    this.reportViewer1.LocalReport.DataSources.Clear();
            //    this.reportViewer1.LocalReport.DataSources.Add(
            //        new Microsoft.Reporting.WinForms.ReportDataSource("BookingSet", _orders)
            //    );

            //    this.reportViewer1.RefreshReport();

            // Microsoft.Reporting.WinForms.ReportParameter[] p = new Microsoft.Reporting.WinForms.ReportParameter[]
            //{
            //     new Microsoft.Reporting.WinForms.ReportParameter("fullname",_booking.CustomerName),
            //     new Microsoft.Reporting.WinForms.ReportParameter("billingDate",_booking.BillingDate.ToString("MM/dd/yyyy")),
            //      new Microsoft.Reporting.WinForms.ReportParameter("bookedDate",_booking.BookedDate.ToString("MM/dd/yyyy")),
            //     new Microsoft.Reporting.WinForms.ReportParameter("totalamount",_booking.BookingReceipt.Sum(o => o.TotalAmount).ToString("N2")),
            //     new Microsoft.Reporting.WinForms.ReportParameter("paymentMethod",_booking.MOP)
            //};
            // this.reportViewer1.LocalReport.SetParameters(p);
            // this.reportViewer1.LocalReport.ReportPath = "AlexisConstruction\\BookingReceipt.rdlc";
            // Microsoft.Reporting.WinForms.ReportDataSource dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("BookingSet", _orders);

            // this.reportViewer1.LocalReport.DataSources.Clear();
            // this.reportViewer1.LocalReport.DataSources.Add(  new Microsoft.Reporting.WinForms.ReportDataSource("BookingSet", _orders));


            // this.reportViewer1.RefreshReport();
        }
    }
}
