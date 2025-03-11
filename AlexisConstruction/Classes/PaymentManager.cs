using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AlexisConstruction.Classes
{
    public class PaymentManager
    {
        public bool ProcessPayment(Bookings payment)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "UPDATE Booking SET PaymentStatus = 'Paid', PaymentMethod = 'Cash' WHERE BookingID = @bookingID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@bookingID", payment.BookingID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Bookings GetBillingInfo(int billingID)
        {
            Bookings billing = null;
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT * FROM Booking WHERE BookingID = @bookingID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@bookingID", billingID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            billing = new Bookings
                            {
                                BookingID = (int)reader["BookingID"],
                                BillingDate = (DateTime)reader["BillingDate"],
                                TotalAmount = (decimal)reader["TotalAmount"],
                                PaymentStatus = reader["PaymentStatus"].ToString(),
                                PaymentMethod = reader["PaymentMethod"].ToString()
                            };
                        }
                    }
                }
            }
            return billing;
        }
    }
}
