using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class Display
    {

        public void GetAllServices(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services", con))
                {
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        reader.Fill(dt);
                        grid.DataSource =  dt;

                        if (grid.Columns["ServiceID"] != null)
                            grid.Columns["ServiceID"].Visible = false;
                       
                    }
                }
            }
        }
        public void GetClients(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT ClientID, FirstName, LastName, CountryCode,ContactNumber, Email, Address FROM Clients";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    da.Fill(table);

                    grid.DataSource = table;

                }
            }
        }
        public void GetAllPayments(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = @"SELECT BookingID, c.Firstname + '' + c.Lastname AS ClientName,BillingDate,BookedDate,TotalAmount,PaymentStatus,PaymentMethod FROM Booking bk
                                    JOIN Clients c ON c.ClientID = bk.ClientID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    da.Fill(table);

                    grid.DataSource = table;

                    if (grid.Columns["ServiceID"] != null)
                        grid.Columns["ServiceID"].Visible = false;

                }
            }
        }
        public void GetAllInventory(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = "SELECT * FROM Inventory";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    da.Fill(table);
                    grid.DataSource = table;

                    if (grid.Columns["InventoryID"] != null)
                        grid.Columns["InventoryID"].Visible = false;
                    if (grid.Columns["ServiceName"] != null)
                        grid.Columns["ServiceName"].Visible = false;
                }
            }
        }
        public void LoadAllBillingRecords(DataGridView grid)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();
                string query = @"SELECT bk.BookingID, c.Firstname + ' ' + c.Lastname AS ClientName,c.ContactNumber,c.Address,
                                        bk.BillingDate, bk.BookedDate,bk.TotalAmount, bk.PaymentStatus, bk.PaymentMethod ,STRING_AGG(s.ServiceName,',') AS ServicesAvailed
                                FROM Booking bk
                                JOIN Clients c ON bk.ClientID = c.ClientID
                                JOIN BookingDetails bd ON bk.BookingID = bd.BookingID
                                JOIN Services s ON bd.ServiceID = s.ServiceID
                                GROUP BY bk.BookingID, c.Firstname, c.Lastname, c.ContactNumber, 
                                  c.Address, bk.BillingDate, bk.BookedDate, bk.TotalAmount, 
                                  bk.PaymentStatus, bk.PaymentMethod;";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;
                //if (grid.Columns["BookingID"] != null)
                //    grid.Columns["BookingID"].Visible = false;
            }
        }
        public void LoadWeeklySchedule(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = @"SELECT c.Firstname +' '+ c.Lastname AS ClientName , bk.BookedDate,s.ServiceID,s.ServiceName,bd.HoursRendered,s.HourlyRate,
                         DATEADD (HOUR,bd.HoursRendered,bk.BookingDate) AS EstimatedEndTime
                         FROM BookingDetails bd
                         JOIN Clients c ON bd.ClientID = c.ClientID
                         JOIN Booking bk ON bk.BookingID = bd.BookingID
                         JOIN Services s ON bd.ServiceID = s.ServiceID 
                         WHERE DATEPART (WEEK,bk.BookedDate) = DATEPART (WEEK,GETDATE()) 
                         AND DATEPART (YEAR,bk.BookedDate) = DATEPART(YEAR, GETDATE())
                         ORDER BY bk.BookedDate";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid.DataSource = dt;

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

                string query = @"SELECT i.InventoryID,i.ItemName
                                FROM Inventory i
                                JOIN ServiceMaterials sm ON sm.InventoryID = i.InventoryID 
                                JOIN  Services s ON s.ServiceID = sm.ServiceID
                                WHERE s.ServiceID = @serviceID AND @endTime > GETDATE()";

                SqlCommand cmd = new SqlCommand(query, con);
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

                string query = @"UPDATE Inventory 
                         SET Quantity = Quantity + 1
                         FROM Inventory i
                         JOIN ServiceMaterials sm ON sm.InventoryID = i.InventoryID
                         JOIN BookingDetails bd ON sm.ServiceID = bd.ServiceID
                         JOIN Booking bk ON bd.BookingID = bk.BookingID
                         WHERE DATEADD(HOUR, bd.HoursRendered, bk.BookingDate) <= GETDATE() 
                         AND bk.Status = 'Scheduled'
                         AND DATEPART (WEEK,bk.BookedDate) = DATEPART (WEEK,GETDATE()) 
                         AND DATEPART (YEAR,bk.BookedDate) = DATEPART(YEAR, GETDATE())";

                SqlCommand cmd = new SqlCommand(query, con);
                int rowAffected = cmd.ExecuteNonQuery();

                if(rowAffected >0)
                {
                    string updatequary = @"UPDATE Booking 
                         SET Status = 'Completed'
                         FROM Booking bk 
                         JOIN BookingDetails bd  ON bk.BookingID = bd.BookingID
                         WHERE DATEADD(HOUR, bd.HoursRendered, bk.BookingDate) <= GETDATE()
                         AND bk.Status = 'Scheduled'
                         AND DATEPART (WEEK,bk.BookedDate) = DATEPART (WEEK,GETDATE()) 
                         AND DATEPART (YEAR,bk.BookedDate) = DATEPART(YEAR, GETDATE())";

                    SqlCommand updatecmd = new SqlCommand(updatequary, con);
                    updatecmd.ExecuteNonQuery();
                }
                if( grid != null)
                {
                    LoadWeeklySchedule(grid);
                }
            }
        }
    }
}
