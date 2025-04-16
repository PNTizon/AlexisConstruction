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
                    using (SqlCommand checkCmd = new SqlCommand("CheckAvailability", con))
                    {
                        checkCmd.CommandType = CommandType.StoredProcedure;
                        checkCmd.Parameters.AddWithValue("@ServiceID", serviceID);

                        int isAvailable = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (isAvailable == 0)
                        {
                            return 0;
                        }
                    }
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

                        int serviceIDfromRow = Convert.ToInt32(row.Cells["ServiceID"].Value);

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

                        using (SqlCommand invenotryCmd = new SqlCommand("UpdateQuantity", con))
                        {
                            invenotryCmd.CommandType = CommandType.StoredProcedure;
                            invenotryCmd.Parameters.AddWithValue("@serviceID", serviceIDfromRow);
                            invenotryCmd.ExecuteNonQuery();
                        }
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
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
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

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
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

        public bool IsTimeSlotAvailable(DateTime bookedDate, int hoursRendered)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CheckTimeSlotAvailability", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookedDate", bookedDate.Date);
                    cmd.Parameters.AddWithValue("@StartTime", bookedDate.TimeOfDay);
                    cmd.Parameters.AddWithValue("@EndTime", bookedDate.AddHours(hoursRendered).TimeOfDay);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 0;
                }
            }
            catch { throw; }
        }

        public bool IsTimeAllowedRange(DateTime bookingTime)
        {
            TimeSpan time = bookingTime.TimeOfDay;
            TimeSpan starttime = new TimeSpan(8, 0, 0);
            TimeSpan endTime = new TimeSpan(15, 0, 0);

            return time >= starttime && time <= endTime;
        }
        public bool IsWorkEndTime(DateTime bookingTime, int hourToAdd)
        {
            TimeSpan workStart = new TimeSpan(8, 0, 0);
            TimeSpan lunchStart = new TimeSpan(12, 0, 0);
            TimeSpan lunchEnd = new TimeSpan(13, 0, 0);
            TimeSpan workEnd = new TimeSpan(17, 0, 0);

            TimeSpan bookingTimeOfDay = bookingTime.TimeOfDay;

            if (bookingTimeOfDay < workStart || bookingTimeOfDay > workEnd ||
                (bookingTimeOfDay >= lunchStart && bookingTimeOfDay < lunchEnd))
            {
                return false;
            }

            DateTime endTime = bookingTime.AddHours(hourToAdd);
            TimeSpan endTimeOfDay = endTime.TimeOfDay;

            if (endTimeOfDay > workEnd || (bookingTimeOfDay < lunchStart && endTimeOfDay > lunchStart))
            {
                return false;
            }

            return true;
        }

        public List<DateTime> GetAvailableTimeSlot(DateTime date, int serviceHours)
        {
            List<DateTime> availableSlots = new List<DateTime>();

            TimeSpan[] startTimes = new[]
            {
                new TimeSpan (8,0,0),
                new TimeSpan (9,0,0),
                new TimeSpan (10,0,0),
                new TimeSpan (11,0,0),
                new TimeSpan (13,0,0),
                new TimeSpan (14,0,0),
                new TimeSpan (15,0,0),
                new TimeSpan (16,0,0),
            };

            foreach (TimeSpan startTime in startTimes)
            {
                DateTime potentialSlot = date.Date.Add(startTime);

                if (potentialSlot < DateTime.Now)
                {
                    continue;
                }

                if (IsWorkEndTime(potentialSlot, serviceHours) &&
                    IsTimeSlotAvailable(potentialSlot, serviceHours))
                {
                    availableSlots.Add(potentialSlot);
                }
            }

            return availableSlots;
        }
    }
}
