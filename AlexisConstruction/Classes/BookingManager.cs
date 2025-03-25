using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class BookingManager
    {
        public int ScheduleBooking(int clientID, DateTime bookedDate, DataGridView grid, int serviceID)
        {
            int bookingID = 0;
            decimal totalAmount = 0;

            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                try
                {
                    using (SqlCommand InsertCmd = new SqlCommand("InsertNewBooking", con))
                    {
                        InsertCmd.CommandType = CommandType.StoredProcedure;
                        InsertCmd.Parameters.AddWithValue("@ClientID", clientID);
                        InsertCmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                        InsertCmd.Parameters.AddWithValue("@BookedDate", bookedDate);
                        InsertCmd.Parameters.AddWithValue("@Status", "Scheduled");
                        InsertCmd.Parameters.AddWithValue("@TotalAmount", 0);
                        bookingID = (int)InsertCmd.ExecuteScalar();
                    }

                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.IsNewRow) continue;

                        decimal serviceAmount = Convert.ToDecimal(row.Cells["HoursRendered"].Value) *
                                          Convert.ToDecimal(row.Cells["HourlyRate"].Value);
                        totalAmount += serviceAmount;

                        using (SqlCommand detailCmd = new SqlCommand("InsertBookingDetails", con))
                        {
                            detailCmd.CommandType = CommandType.StoredProcedure;
                            detailCmd.Parameters.AddWithValue("@BookingID", bookingID);
                            detailCmd.Parameters.AddWithValue("@ClientID", clientID);
                            detailCmd.Parameters.AddWithValue("@ServiceID", row.Cells["ServiceID"].Value);
                            detailCmd.Parameters.AddWithValue("@HoursRendered", row.Cells["HoursRendered"].Value);
                            detailCmd.Parameters.AddWithValue("@BookedDate", bookedDate);
                            detailCmd.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand invenotryCmd = new SqlCommand("UpdateQuantity", con))
                    {
                        invenotryCmd.CommandType = CommandType.StoredProcedure;
                        invenotryCmd.Parameters.AddWithValue("@serviceID", serviceID);
                        int rowAffected = invenotryCmd.ExecuteNonQuery();
                    }
                    using (SqlCommand updateCmd = new SqlCommand("UpdateAmount", con))
                    {
                        updateCmd.CommandType = CommandType.StoredProcedure;
                        updateCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        updateCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        updateCmd.ExecuteNonQuery();
                    }

                    if (bookingID > 0)
                    {
                        GenerateBilling(bookingID, totalAmount);
                    }

                    return bookingID;
                }
                catch
                {
                    return 0;
                    throw;
                }
            }
        }
        public bool IsInventoryAvailable(int serviceID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("CheckAvailability", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ServiceID", serviceID);
                        int count = (int)cmd.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }
            catch { throw; }
        }
        public void GenerateBilling(int bookingID, decimal totalAmount)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdaetBilling", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BillingDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch { throw; }
        }
        public bool IsDateAlreadyBooked(DateTime bookedDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("CheckBookedDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookedDate", bookedDate.Date);
                    cmd.Parameters.AddWithValue("@BookedTime", bookedDate.TimeOfDay);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch { throw; }
        }
        public void UpdatePaymentStatus(int bookingID)
        {
            try
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
            catch { throw; }
        }
        public static List<Client> LoadClients()
        {
            List<Client> customer = new List<Client>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ConcatinateName", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.Add(new Client
                            {
                                ClientID = Convert.ToInt32(reader["ClientID"].ToString()),
                                Fullname = reader["Fullname"].ToString(),
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return customer;
        }
        public static List<Services> LoadServices()
        {
            List<Services> service = new List<Services>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ServiceID, ServiceName, HourlyRate FROM Services", con);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            service.Add(new Services
                            {
                                ServiceID = reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                ServiceName = reader["ServiceName"].ToString(),
                                HourlyRate = Convert.ToDecimal(reader["HourlyRate"].ToString())
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return service;
        }
        public static List<Orders> GetServiceDetails(int bookingID)
        {
            List<Orders> orders = new List<Orders>();

            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("GetServiceDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookingID", bookingID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Orders
                            {
                                ServiceName = reader["ServiceName"].ToString(),
                                HourlyRate = Convert.ToInt32(reader["HourlyRate"]),
                                HoursRendered = Convert.ToInt32(reader["HoursRendered"]),
                            });
                        }
                    }
                }
            }
            catch { throw; }
            return orders;
        }
        public bool IsTimeAllowedRange(DateTime bookingTime)
        {
            TimeSpan time = bookingTime.TimeOfDay;
            TimeSpan starttime = new TimeSpan(8, 0, 0);
            TimeSpan endTime = new TimeSpan(15, 0, 0);

            return time >= starttime && time <= endTime;
        }
        //public bool IsWorkEndTime(DateTime bookingtime, int hourToAdd)
        //{
        //    TimeSpan workStart = new TimeSpan(8, 0, 0);
        //    TimeSpan endWord = new TimeSpan(17, 0, 0);

        //    TimeSpan remainingTimeofWork = bookingtime.TimeOfDay;

        //    if (remainingTimeofWork < workStart || remainingTimeofWork > endWord)
        //    {
        //        return false; // Booking exceeds working hours
        //    }

        //    DateTime endtime = bookingtime.AddHours(hourToAdd);

        //    if (endtime.TimeOfDay > endWord)
        //    {
        //        return false;
        //    }

        //    return true;

        //     if (!bookingManager.IsWorkEndTime(BookingDetails.BookedDate, Convert.ToInt32(nudHoursRendered.Value)))
        //            {
        //                MessageBox.Show("Booking time must be between 7:00 AM and 5:00 PM.");
        //                return;
        //            }
        ////}
    }
}
