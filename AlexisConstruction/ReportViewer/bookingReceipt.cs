using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Reporting.WinForms;

namespace AlexisConstruction.Forms
{
    public partial class bookingReceipt : Form
    {
        BookingDetails _booking;
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
                    cmd.Parameters.AddWithValue("@bookingID", _booking.BookingsID);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    this.reportViewer1.LocalReport.ReportPath = "BookingReceipt.rdlc";

                    this.reportViewer1.LocalReport.DataSources.Clear();

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
        }
    }
}
