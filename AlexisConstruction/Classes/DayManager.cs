using AlexisConstruction.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class DayManager
    {
        private bool hasEvent;

        public void DisplayEvent(Label lblEvent,ToolTip toolTip1,Label lblDays,Control thisTooltip )
        {
            try
            {
                int year = Calendar.staticYear;
                int month = Calendar.staticMonth;
                int day = Convert.ToInt32(lblDays.Text);

                DateTime date = new DateTime(year, month, day);

                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    string query = @"SELECT * FROM Booking WHERE CONVERT (date,BookedDate) = @BookedDate";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BookedDate", date.Date);

                    int eventCount = Convert.ToInt32(cmd.ExecuteScalar());
                    hasEvent = eventCount > 0;

                    if (hasEvent)
                    {
                        string detailsEvent = @"SELECT bk.BookingID, c.Firstname + ' ' + c.Lastname AS ClientName, bd.HoursRendered, s.ServiceName, s.HourlyRate, bk.TotalAmount,
                                                bk.BillingDate ,bk.Status, bk.PaymentStatus, bk.PaymentMethod
                                                FROM BookingDetails bd
                                                JOIN Services s ON s.ServiceID = bd.ServiceID
                                                JOIN Booking bk ON bk.BookingID = bd.BookingID 
                                                JOIN Clients c ON bd.ClientID = c.ClientID
                                                WHERE CONVERT (date,bk.BookedDate) = @BookedDate";

                        SqlCommand cmdDetails = new SqlCommand(detailsEvent, con);
                        cmdDetails.Parameters.AddWithValue("@BookedDate", date.Date);

                        StringBuilder eventuilder = new StringBuilder();
                        using (SqlDataReader reader = cmdDetails.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventuilder.AppendLine($"Booking ID: {reader["BookingID"]}");
                                eventuilder.AppendLine($"Client Name: {reader["ClientName"]}");
                                eventuilder.AppendLine($"Hours Rendered: {reader["HoursRendered"]}");
                                eventuilder.AppendLine($"Service Name: {reader["ServiceName"]}");
                                eventuilder.AppendLine($"Hourly Rate: {reader["HourlyRate"]}");
                                eventuilder.AppendLine($"Total Amount: {reader["TotalAmount"]}");
                                eventuilder.AppendLine($"Billing Date: {reader["BillingDate"]}");
                                eventuilder.AppendLine($"Status: {reader["Status"]}");
                                eventuilder.AppendLine($"Payment Status: {reader["PaymentStatus"]}");
                                eventuilder.AppendLine($"Payment Method: {reader["PaymentMethod"]}");

                                lblEvent.Text = eventuilder.ToString();
                                if (reader["Status"].ToString() == "Scheduled")
                                {
                                    lblEvent.BackColor = Color.FromArgb(166, 216, 168);
                                }
                                else if (reader["Status"].ToString() == "Completed")
                                {
                                    lblEvent.BackColor = Color.FromArgb(134, 197, 249);
                                }
                                else if (reader["Status"].ToString() == "Cancelled")
                                {
                                    lblEvent.BackColor = Color.FromArgb(249, 165, 159);
                                }
                            }
                        }
                        //toolTip1.SetToolTip(thisTooltip, eventuilder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error  occured");
            }
        }
        public Color GetDayColor(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return Color.Red;
            }
            else
            {
                return Color.Black;
            }
        }

    }
}
