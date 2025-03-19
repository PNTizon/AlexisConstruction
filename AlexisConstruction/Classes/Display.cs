using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class Display
    {
        #region Unused Code 
        //public void GetAllServices(DataGridView grid)
        //{
        //    using (SqlConnection con = new SqlConnection(Connection.Database))
        //    {
        //        con.Open();
        //        using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services", con))
        //        {
        //            using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
        //            {
        //                DataTable dt = new DataTable();
        //                reader.Fill(dt);
        //                grid.DataSource =  dt;

        //                if (grid.Columns["ServiceID"] != null)
        //                    grid.Columns["ServiceID"].Visible = false;

        //            }
        //        }
        //    }
        //}
        //
        //public void GetAllPayments(DataGridView grid)
        //{
        //    using (SqlConnection con = new SqlConnection(Connection.Database))
        //    {
        //        con.Open();
        //        string query = @"SELECT BookingID, c.Firstname + '' + c.Lastname AS ClientName,BillingDate,BookedDate,TotalAmount,PaymentStatus,PaymentMethod FROM Booking bk
        //                            JOIN Clients c ON c.ClientID = bk.ClientID";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable table = new DataTable();
        //            da.Fill(table);

        //            grid.DataSource = table;

        //            if (grid.Columns["ServiceID"] != null)
        //                grid.Columns["ServiceID"].Visible = false;

        //        }
        //    }
        //}
        //public void GetAllInventory(DataGridView grid)
        //{
        //    using (SqlConnection con = new SqlConnection(Connection.Database))
        //    {
        //        con.Open();

        //        string query = "SELECT * FROM Inventory";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable table = new DataTable();
        //            da.Fill(table);
        //            grid.DataSource = table;

        //            if (grid.Columns["InventoryID"] != null)
        //                grid.Columns["InventoryID"].Visible = false;
        //            if (grid.Columns["ServiceName"] != null)
        //                grid.Columns["ServiceName"].Visible = false;
        //        }
        //    }
        //}
        //public void LoadAllBillingRecords(DataGridView grid)
        //{
        //    using (SqlConnection conn = new SqlConnection(Connection.Database))
        //    {
        //        conn.Open();
        //        string query = @"SELECT bk.BookingID, c.Firstname + ' ' + c.Lastname AS ClientName,c.ContactNumber,c.Address,
        //                                bk.BillingDate, bk.BookedDate,bk.TotalAmount, bk.PaymentStatus, bk.PaymentMethod ,STRING_AGG(s.ServiceName,',') AS ServicesAvailed
        //                        FROM Booking bk
        //                        JOIN Clients c ON bk.ClientID = c.ClientID
        //                        JOIN BookingDetails bd ON bk.BookingID = bd.BookingID
        //                        JOIN Services s ON bd.ServiceID = s.ServiceID
        //                        GROUP BY bk.BookingID, c.Firstname, c.Lastname, c.ContactNumber, 
        //                          c.Address, bk.BillingDate, bk.BookedDate, bk.TotalAmount, 
        //                          bk.PaymentStatus, bk.PaymentMethod;";

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        grid.DataSource = dt;
        //        //if (grid.Columns["BookingID"] != null)
        //        //    grid.Columns["BookingID"].Visible = false;
        //    }
        //}
        //public void GetClients(DataGridView grid)
        //{
        //    using (SqlConnection con = new SqlConnection(Connection.Database))
        //    {
        //        con.Open();
        //        string query = "SELECT ClientID, FirstName, LastName, CountryCode,ContactNumber, Email, Address FROM Clients";
        //        using (SqlCommand cmd = new SqlCommand(query, con))
        //        {
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable table = new DataTable();
        //            da.Fill(table);

        //            grid.DataSource = table;

        //        }
        //    }
        //}

        #endregion

        public void LoadWeeklySchedule(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("WEEKLYSCHEDULE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;

                if (grid.Columns["ServiceID"] != null)
                    grid.Columns["ServiceID"].Visible = false;
                Dictionary<int, DataTable> toolsData = new Dictionary<int, DataTable>();

                foreach (DataRow row in dt.Rows)
                {
                    int serviceID = Convert.ToInt32(row["ServiceID"]);
                    DateTime estimatedEndTime = Convert.ToDateTime(row["EstimatedEndTime"]);

                    if (!toolsData.ContainsKey(serviceID))
                    {
                        toolsData[serviceID] = LoadAssociatedTools(serviceID, estimatedEndTime);
                    }
                }
            }
        }
        public DataTable LoadAssociatedTools(int serviceID, DateTime endTime)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("LoadAssociatedTools", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@endTime", endTime);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
               return dt;

            }
        }
        public void CheckAndUpdateCompletedServices(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
              
                SqlCommand cmd = new SqlCommand("UpdateIventoryAndBooking", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int rowAffected = cmd.ExecuteNonQuery();

                if(rowAffected >0 && grid != null)
                {
                    LoadWeeklySchedule(grid);
                }
            }
        }
    }
}
