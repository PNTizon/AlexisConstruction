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
                    string insertBookingQuery = @"INSERT INTO Booking (ClientID ,BookingDate, BookedDate, Status, TotalAmount, PaymentStatus, PaymentMethod) 
                                                  OUTPUT INSERTED.BookingID
                                                  VALUES (@ClientID ,@BookingDate, @BookedDate, @Status, @TotalAmount, 'Pending', 'Cash')";
                    SqlCommand cmd = new SqlCommand(insertBookingQuery, con);
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

                        string insertDetailQuery = @"INSERT INTO BookingDetails (BookingID, ClientID, ServiceID, HoursRendered, BookedDate)
                                           VALUES (@BookingID, @ClientID, @ServiceID, @HoursRendered, @BookedDate)";
                        SqlCommand detailCmd = new SqlCommand(insertDetailQuery, con);
                        detailCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        detailCmd.Parameters.AddWithValue("@ClientID", clientID);
                        detailCmd.Parameters.AddWithValue("@ServiceID", row.Cells["ServiceID"].Value);
                        detailCmd.Parameters.AddWithValue("@HoursRendered", row.Cells["HoursRendered"].Value);
                        detailCmd.Parameters.AddWithValue("@BookedDate", bookedDate);
                        detailCmd.ExecuteNonQuery();
                    }

                    string updateInventory = @"UPDATE Inventory SET Quantity = Quantity - 1 
                                           WHERE InventoryID IN  (SELECT InventoryID FROM ServiceMaterials s WHERE s.ServiceID = @serviceID)
                                           AND Quantity  > 0";
                    SqlCommand invenotryUpdate = new SqlCommand(updateInventory, con);
                    invenotryUpdate.Parameters.AddWithValue("@serviceID", serviceID);
                    int rowAffected = invenotryUpdate.ExecuteNonQuery();

                    string updateTotalAmountQuery = "UPDATE Booking SET TotalAmount = @TotalAmount WHERE BookingID = @BookingID";
                    SqlCommand updateCmd = new SqlCommand(updateTotalAmountQuery, con);
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
                    string updateBillingQuery = @"UPDATE Booking 
                                              SET BillingDate = @BillingDate, 
                                                  TotalAmount = @TotalAmount 
                                              WHERE BookingID = @BookingID";

                    SqlCommand cmd = new SqlCommand(updateBillingQuery, con);
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

                string query = "SELECT COUNT(*) FROM Booking WHERE CAST( BookedDate AS DATE) = @BookedDate";

                SqlCommand cmd = new SqlCommand(query, con);
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

                string query = "UPDATE Booking SET PaymentStatus = 'Paid' WHERE BookingID = @bookingID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@bookingID", bookingID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
