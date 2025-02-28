using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class BookingManager
    {
        public bool ScheduleBooking(int clientID, DateTime bookingDate, List<BookingDetails> bookingDetails)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                try
                {
                    string insertBookingQuery = "INSERT INTO Bookings (ClientID, BookingDate, Status) OUTPUT INSERTED.BookingID VALUES (@ClientID, @BookingDate, @Status)";
                    SqlCommand cmd = new SqlCommand(insertBookingQuery, con);
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
                    cmd.Parameters.AddWithValue("@Status", "Scheduled");
                    int bookingID = (int)cmd.ExecuteScalar();

                    foreach (var detail in bookingDetails)
                    {
                        string insertDetailsQuery = "INSERT INTO BookingDetails (BookingID, ServiceID, HoursRendered) VALUES (@BookingID, @ServiceID, @HoursRendered)";
                        SqlCommand detailCmd = new SqlCommand(insertDetailsQuery, con);
                        detailCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        detailCmd.Parameters.AddWithValue("@ServiceID", detail.ServiceID);
                        detailCmd.Parameters.AddWithValue("@HoursRendered", detail.HoursRendered);
                        detailCmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }
        }

        public bool GenerateBilling(int bookingID, decimal totalAmount)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                try
                {
                    string insertBillingQuery = "INSERT INTO Billings (BookingID, TotalAmount, BillingDate) VALUES (@BookingID, @TotalAmount, @BillingDate)";
                    SqlCommand cmd = new SqlCommand(insertBillingQuery, con);
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@BillingDate", DateTime.Now);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating billing: {ex.Message}");
                    return false;
                }
            }
        }

    }
}
