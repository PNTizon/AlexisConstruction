using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class EventManager
    {
        public bool EditBoking(ComboBox cmbStatus, ComboBox cmbPaymentStatus,DateTime selectedDate,int bookingID,DataGridView dgvServices)
        {
            decimal totalAmount = 0;
            bool anyUpdate = false;

           
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        foreach (DataGridViewRow row in dgvServices.Rows)
                        {
                            if (row.IsNewRow) continue;
                            if (row.Cells["ServiceID"].Value != null && row.Cells["ServiceID"].Value != DBNull.Value)
                            {
                                int serviceID = Convert.ToInt32(row.Cells["ServiceID"].Value);

                                using (SqlCommand cmd = new SqlCommand("CheckAvailability", con,transaction))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@ServiceID", serviceID);
                                    int isAvailable = Convert.ToInt32(cmd.ExecuteScalar());

                                    if (isAvailable == 0)
                                    {
                                        transaction.Rollback();
                                        return false;
                                    }
                                }
                            }
                        }

                        string query = @"UPDATE Booking SET Status = @Status, PaymentStatus = @PaymentStatus
                                    WHERE BookingID = @BookingID";

                        using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                            cmd.Parameters.AddWithValue("@PaymentStatus", cmbPaymentStatus.Text);
                            cmd.Parameters.AddWithValue("@BookingID", bookingID);
                            cmd.ExecuteNonQuery();

                            foreach (DataGridViewRow row in dgvServices.Rows)
                            {
                                if (row.IsNewRow)
                                    continue;

                                if (row.Cells["Service"].Value != null && row.Cells["ServiceID"].Value != DBNull.Value &&
                                    row.Cells["Hoursrender"].Value != null && row.Cells["Hoursrender"].Value != DBNull.Value &&
                                    row.Cells["Rates"].Value != null && row.Cells["Rates"].Value != DBNull.Value)
                                {
                                    int serviceID = Convert.ToInt32(row.Cells["ServiceID"].Value);
                                    decimal hoursRendered = Convert.ToDecimal(row.Cells["Hoursrender"].Value);
                                    decimal rate = Convert.ToDecimal(row.Cells["Rates"].Value);
                                    decimal serviceAmount = hoursRendered * rate;

                                    totalAmount += serviceAmount;


                                    string check = @"SELECT COUNT(*) FROM BookingDetails WHERE BookingID = @BookingID
                                                AND ServiceID = @ServiceID";
                                    using (SqlCommand checkCmd = new SqlCommand(check, con, transaction))
                                    {
                                        checkCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                        checkCmd.Parameters.AddWithValue("@ServiceID", serviceID);
                                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                        if (count > 0)
                                        {
                                            string serviceQuery = @"UPDATE BookingDetails SET HoursRendered = @HoursRendered
                                                    WHERE BookingID = @BookingID  AND  ServiceID = @ServiceID";
                                            using (SqlCommand serviceCmd = new SqlCommand(serviceQuery, con, transaction))
                                            {
                                                serviceCmd.Parameters.AddWithValue("@HoursRendered", hoursRendered);
                                                serviceCmd.Parameters.AddWithValue("@BookingID", bookingID);
                                                serviceCmd.Parameters.AddWithValue("@ServiceID", serviceID);
                                                anyUpdate |= serviceCmd.ExecuteNonQuery() > 0;
                                            }
                                        }
                                        else
                                        {
                                            string insertQuery = @"INSERT INTO BookingDetails (BookingID,ClientID,ServiceID,HoursRendered,BookedDate)
                                                            VALUES (@BookingID,@ClientID,@ServiceID,@HoursRendered,@BookedDate)";

                                            using (SqlCommand insertcmd = new SqlCommand(insertQuery, con, transaction))
                                            {

                                                insertcmd.Parameters.AddWithValue("@BookingID", bookingID);
                                                insertcmd.Parameters.AddWithValue("@ClientID", CalendarRecords.ClientID);
                                                insertcmd.Parameters.AddWithValue("@HoursRendered", hoursRendered);
                                                insertcmd.Parameters.AddWithValue("@ServiceID", serviceID);
                                                insertcmd.Parameters.AddWithValue("@BookedDate", selectedDate);
                                                anyUpdate |= insertcmd.ExecuteNonQuery() > 0;
                                            }
                                            using (SqlCommand invenotryCmd = new SqlCommand("UpdateQuantity", con, transaction))
                                            {
                                                invenotryCmd.CommandType = CommandType.StoredProcedure;
                                                invenotryCmd.Parameters.AddWithValue("@serviceID", serviceID);
                                                int rowAffected = invenotryCmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    
                                }

                                using (SqlCommand totalAmountCmd = new SqlCommand("UpdateAmount", con, transaction))
                                {
                                    totalAmountCmd.CommandType = CommandType.StoredProcedure;
                                    totalAmountCmd.Parameters.AddWithValue("@TotalAmount",totalAmount);
                                    totalAmountCmd.Parameters.AddWithValue("@BookingID", CalendarRecords.BookingsID);
                                    totalAmountCmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return anyUpdate ;
                        }
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show(ex.Message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public void LoadBookingDate(DateTime bookedDate, TextBox txtBookingID, TextBox txtClientID, ComboBox cmbStatus, ComboBox cmbPaymentStatus, DataGridView dgvServices)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    string query = @"SELECT bk.BookingID, c.ClientID, c.Firstname + ' ' + c.Lastname AS ClientName, bd.HoursRendered, s.ServiceID,
                                s.ServiceName, s.HourlyRate, bk.TotalAmount, bk.BillingDate,
                                bk.Status, bk.PaymentStatus, bk.PaymentMethod
                                FROM BookingDetails bd
                                JOIN Services s ON s.ServiceID = bd.ServiceID
                                JOIN Booking bk ON bk.BookingID = bd.BookingID 
                                JOIN Clients c ON bd.ClientID = c.ClientID
                                WHERE CONVERT(date, bk.BookedDate) = @BookedDate";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BookedDate", bookedDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtBookingID.Text = row["BookingID"].ToString();
                        txtClientID.Text = row["ClientID"].ToString();
                        cmbStatus.Text = row["Status"].ToString();
                        cmbPaymentStatus.Text = row["PaymentStatus"].ToString();
                        dgvServices.DataSource = dt;

                    }
                    else
                    {
                        txtBookingID.Text = string.Empty;
                        txtClientID.Text = string.Empty;
                        cmbStatus.SelectedIndex = -1;  
                        cmbPaymentStatus.SelectedIndex = -1;  
                        dgvServices.DataSource = null; 
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public bool AddService(DataGridView dgvServices, int RowIndex, Action updateAmount)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    CalendarRecords.ServiceName = dgvServices.Rows[RowIndex].Cells["Service"].Value?.ToString();
                    string query = "SELECT ServiceID,HourlyRate FROM Services WHERE ServiceName = @ServiceName";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ServiceName", CalendarRecords.ServiceName);

                    using (SqlDataReader result = cmd.ExecuteReader())
                    {

                        if (result.Read())
                        {

                            dgvServices.Rows[RowIndex].Cells["ServiceID"].Value = result.GetInt32(0);
                            dgvServices.Rows[RowIndex].Cells["Rates"].Value = result.GetDecimal(1);

                            var hoursCell = dgvServices.Rows[RowIndex].Cells["Hoursrender"];
                            if (hoursCell.Value != null && hoursCell.Value != DBNull.Value)
                            {
                                updateAmount();
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch
            {
                throw;
            }
            
        }
        public bool AreAllServicesAvailable(DataGridView grid)
        {
            using(SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                foreach(DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (row.Cells["ServiceID"].Value != null && row.Cells["ServiceID"].Value != DBNull.Value)
                    {
                        int serviceID = Convert.ToInt32(row.Cells["ServiceID"].Value);

                        using(SqlCommand cmd = new SqlCommand("CheckAvailability",con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ServiceID", serviceID);
                            int isAvailable = Convert.ToInt32(cmd.ExecuteScalar());

                            if(isAvailable == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}
