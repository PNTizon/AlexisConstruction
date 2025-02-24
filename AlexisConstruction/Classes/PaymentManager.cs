using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisConstruction.Classes
{
    public class PaymentManager
    {
        public List<Billing> GetBillingList()
        {
            var billingList = new List<Billing>();
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT * FROM Billings";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            billingList.Add(new Billing
                            {
                                BillingID = (int)reader["BillingID"],
                                BookingID = (int)reader["BookingID"],
                                BillingDate = (DateTime)reader["BillingDate"],
                                PaymentStatus = Enum.TryParse(reader["PaymentStatus"].ToString(), out PaymentStatus status) ? status : PaymentStatus.Pending
                            });
                        }
                    }
                }
            }
            return billingList;
        }

        public bool ProcessPayment(Payment payment)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "INSERT INTO Payments (BillingID, PaymentDate, AmountPaid, PaymentMethod) VALUES (@billingID, @paymentDate, @amountPaid, 'Cash')";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@billingID", payment.BillingID);
                    cmd.Parameters.AddWithValue("@paymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@amountPaid", payment.AmountPaid);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public Billing GetBillingInfo(int billingID)
        {
            Billing billing = null;
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT * FROM Billings WHERE BillingID = @billingID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@billingID", billingID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            billing = new Billing
                            {
                                BillingID = (int)reader["BillingID"],
                                BookingID = (int)reader["BookingID"],
                                BillingDate = (DateTime)reader["BillingDate"],
                                PaymentStatus = Enum.TryParse(reader["PaymentStatus"].ToString(), out PaymentStatus status) ? status : PaymentStatus.Pending
                            };
                        }
                    }
                }
            }
            return billing;
        }
    }
}
