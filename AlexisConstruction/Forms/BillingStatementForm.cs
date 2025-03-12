using AlexisConstruction.Classes;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class BillingStatementForm : Form
    {
        private Display display = new Display();
        public BillingStatementForm()
        {
            InitializeComponent();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadBillingReport(txtSearchBox.Text);

        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadBillingByDate(dtpFrom.Value, dtpTo.Value);
        }

        public void LoadBillingReport(string clientName)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();
                string query = @"SELECT bk.BookingID, 
                                c.Firstname + ' ' + c.Lastname AS ClientName,
                                c.ContactNumber, c.Address, 
                                bk.BillingDate, bk.BookedDate, 
                                SUM(bd.HoursRendered) AS TotalHours,
                                bk.TotalAmount, bk.PaymentStatus, bk.PaymentMethod
                        FROM Booking bk
                        JOIN Clients c ON bk.ClientID = c.ClientID
                        JOIN BookingDetails bd ON bk.BookingID = bd.BookingID
                        WHERE CONCAT(c.Firstname, ' ', c.Lastname) LIKE @SearchName
                        GROUP BY bk.BookingID, c.Firstname, c.Lastname, 
                                 c.ContactNumber, c.Address, 
                                 bk.BillingDate, bk.BookedDate, 
                                 bk.TotalAmount, bk.PaymentStatus, bk.PaymentMethod";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchName", "%" + clientName + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void LoadBillingByDate(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();
                string query = @" SELECT bk.BookingID, c.Firstname + ' ' + c.Lastname AS ClientName, c.ContactNumber,
                                c.Address, bk.BillingDate, bk.BookedDate,SUM(bd.HoursRendered) AS TotalHours,bk.TotalAmount ,
                                        bk.PaymentStatus,bk.PaymentMethod
                                FROM Booking bk
                                JOIN Clients c ON bk.ClientID = c.ClientID
                               JOIN BookingDetails bd ON bk.BookingID = bd.BookingID
                        WHERE bk.BillingDate BETWEEN @StartDate AND @EndDate
                        GROUP BY bk.BookingID, c.Firstname, c.Lastname, 
                                 c.ContactNumber, c.Address, 
                                 bk.BillingDate, bk.BookedDate, 
                                 bk.TotalAmount, bk.PaymentStatus, bk.PaymentMethod";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
        private void BillingStatementForm_Load(object sender, EventArgs e)
        {
            display.LoadAllBillingRecords(dataGridView1);
        }
        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {
        //        OrderDetails orders = new OrderDetails();

        //        DataGridViewRow row = dataGridView1.SelectedRows[0];

        //        orders.CustomerName = row.Cells["ClientName"].Value.ToString();
        //        orders.BookingsID = Convert.ToInt32(row.Cells["BookingID"].Value);
        //        orders.BillingDate = Convert.ToDateTime(row.Cells["BookedDate"].Value);
        //        orders.ContactNumber = row.Cells["ContactNumber"].Value.ToString();
        //        orders.Address = row.Cells["Address"].Value.ToString();
        //        orders.MOP = "Cash";

        //        orders.ReceiptOrder = GetServiceDetails(orders.BookingsID,orders.BillingDate);

        //        using (printReceipt receipt = new printReceipt(orders, orders.ReceiptOrder))
        //        {
        //            receipt.ShowDialog();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select a record to generate a receipt.");
        //    }
        //}
        //private List<Orders> GetServiceDetails(int bookingID,DateTime bookedate)
        //{
        //    List<Orders> services = new List<Orders>();

        //    using(SqlConnection con = new SqlConnection(Connection.Database))
        //    {
        //        con.Open();

        //        string query = @"SELECT s.ServiceName ,bd.HoursRendered,s.HourlyRate,(bd.HoursRendered * s.HourlyRate) AS TotalAmount
        //                        FROM BookingDetails bd
        //                        JOIN Services s ON bd.ServiceID = s.ServiceID
        //                        WHERE bd.ClientID = @clientID AND CAST  (bd.BookedDate AS DATE) = @bookedDate";

        //        SqlCommand cmd = new SqlCommand(query, con);
        //        cmd.Parameters.AddWithValue("@clientID", bookingID);
        //        cmd.Parameters.AddWithValue("@bookedDate", bookedate);
        //        using(SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while(reader.Read())
        //            {
        //                services.Add(new Orders
        //                {
        //                    ServiceName = reader["ServiceName"].ToString(),
        //                    HourlyRate = Convert.ToInt32(reader["HourlyRate"]),
        //                    HoursRendered = Convert.ToInt32(reader["HoursRendered"]),
        //                });
        //            }
        //        }
        //    }
        //    return services;
        //}

    }
}
