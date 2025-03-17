using AlexisConstruction.Classes;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
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
                
                SqlCommand cmd = new SqlCommand("LoadBillingByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
                
                SqlCommand cmd = new SqlCommand("LoadBillingByDate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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
            // TODO: This line of code loads data into the 'dataSet2.BILLINGSTATEMENT' table. You can move, or remove it, as needed.
            this.bILLINGSTATEMENTTableAdapter.Fill(this.dataSet2.BILLINGSTATEMENT);
        }
        #region Unused Receipt Function
        //    private void btnPrint_Click(object sender, EventArgs e)
        //    {
        //        if (dataGridView1.SelectedRows.Count > 0)
        //        {
        //            OrderDetails orders = new OrderDetails();

        //            DataGridViewRow row = dataGridView1.SelectedRows[0];

        //            orders.CustomerName = row.Cells["ClientName"].Value.ToString();
        //            orders.BookingsID = Convert.ToInt32(row.Cells["BookingID"].Value);
        //            orders.BillingDate = Convert.ToDateTime(row.Cells["BookedDate"].Value);
        //            orders.ContactNumber = row.Cells["ContactNumber"].Value.ToString();
        //            orders.Address = row.Cells["Address"].Value.ToString();
        //            orders.MOP = "Cash";

        //            orders.ReceiptOrder = GetServiceDetails(orders.BookingsID, orders.BillingDate);

        //            //using (printReceipt receipt = new printReceipt(orders, orders.ReceiptOrder))
        //            //{
        //            //    receipt.ShowDialog();
        //            //}
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please select a record to generate a receipt.");
        //        }
        //    }
        //    private List<Orders> GetServiceDetails(int bookingID, DateTime bookedate)
        //    {
        //        List<Orders> services = new List<Orders>();

        //        using (SqlConnection con = new SqlConnection(Connection.Database))
        //        {
        //            con.Open();

        //            string query = @"SELECT s.ServiceName ,bd.HoursRendered,s.HourlyRate,(bd.HoursRendered * s.HourlyRate) AS TotalAmount
        //                            FROM BookingDetails bd
        //                            JOIN Services s ON bd.ServiceID = s.ServiceID
        //                            WHERE bd.ClientID = @clientID AND CAST  (bd.BookedDate AS DATE) = @bookedDate";

        //            SqlCommand cmd = new SqlCommand(query, con);
        //            cmd.Parameters.AddWithValue("@clientID", bookingID);
        //            cmd.Parameters.AddWithValue("@bookedDate", bookedate);
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    services.Add(new Orders
        //                    {
        //                        ServiceName = reader["ServiceName"].ToString(),
        //                        HourlyRate = Convert.ToInt32(reader["HourlyRate"]),
        //                        HoursRendered = Convert.ToInt32(reader["HoursRendered"]),
        //                    });
        //                }
        //            }
        //        }
        //        return services;
        //    }
        //}
        #endregion

    }
}
