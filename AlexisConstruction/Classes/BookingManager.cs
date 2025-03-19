using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class BookingManager
    {
        public int ScheduleBooking(int clientID, DateTime bookedDate, DataGridView grid,int serviceID)
        {
            int bookingID = 0;
            decimal totalAmount = 0;

            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand("InsertNewBooking", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BookedDate", bookedDate);
                    cmd.Parameters.AddWithValue("@Status", "Scheduled");
                    cmd.Parameters.AddWithValue("@TotalAmount", 0);
                    bookingID = (int)cmd.ExecuteScalar();

                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.IsNewRow) continue;

                        decimal serviceAmount = Convert.ToDecimal(row.Cells["HoursRendered"].Value) *
                                          Convert.ToDecimal(row.Cells["HourlyRate"].Value);
                        totalAmount += serviceAmount;

                        SqlCommand detailCmd = new SqlCommand("InsertBookingDetails", con);
                        detailCmd.CommandType = CommandType.StoredProcedure;
                        detailCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        detailCmd.Parameters.AddWithValue("@ClientID", clientID);
                        detailCmd.Parameters.AddWithValue("@ServiceID", row.Cells["ServiceID"].Value);
                        detailCmd.Parameters.AddWithValue("@HoursRendered", row.Cells["HoursRendered"].Value);
                        detailCmd.Parameters.AddWithValue("@BookedDate", bookedDate);
                        detailCmd.ExecuteNonQuery();
                    }
                   
                    SqlCommand invenotryUpdate = new SqlCommand("UpdateQuantity", con);
                    invenotryUpdate.CommandType = CommandType.StoredProcedure;
                    invenotryUpdate.Parameters.AddWithValue("@serviceID", serviceID);
                    int rowAffected = invenotryUpdate.ExecuteNonQuery();
                 
                    SqlCommand updateCmd = new SqlCommand("UpdateAmount", con);
                    updateCmd.CommandType = CommandType.StoredProcedure;
                    updateCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    updateCmd.Parameters.AddWithValue("@BookingID", bookingID);
                    updateCmd.ExecuteNonQuery();


                    if (bookingID > 0)
                    {
                        GenerateBilling(bookingID, totalAmount);
                    }

                    return bookingID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return 0;
                }
            }
        }

        public void GenerateBilling(int bookingID, decimal totalAmount)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("UpdaetBilling", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillingDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public bool IsDateAlreadyBooked(DateTime bookedDate)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("CheckBookedDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookedDate", bookedDate.Date);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public void UpdatePaymentStatus(int bookingID)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UpdatePaymentStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookingID", bookingID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
