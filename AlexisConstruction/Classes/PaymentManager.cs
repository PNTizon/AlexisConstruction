using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class PaymentManager
    {
        public bool ProcessPayment(Bookings payment)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdatePayment", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bookingID", payment.BookingID);
                        cmd.Parameters.AddWithValue("@billingdate", DateTime.Now);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public Bookings GetBillingInfo(int billingID)
        {
            Bookings billing = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetBooking", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bookingID", billingID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                billing = new Bookings
                                {
                                    BookingID = Convert.ToInt32(reader["BookingID"]),
                                    BillingDate = reader["BillingDate"] != DBNull.Value ? Convert.ToDateTime(reader["BillingDate"]) : DateTime.Now,
                                    TotalAmount = (decimal)reader["TotalAmount"],
                                    PaymentStatus = reader["PaymentStatus"].ToString(),
                                    PaymentMethod = reader["PaymentMethod"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return billing;
        }
        public void SeacrhRecords (string search,DataGridView grid)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using(SqlCommand cmd = new SqlCommand("SearchRecords",con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SearchInput", search?.Trim() ?? (object)DBNull.Value);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if(table.Rows.Count > 0 )
                        {
                            grid.DataSource = table;
                        }
                    }
                }
            }
            catch { throw; }
        }
    }
}
