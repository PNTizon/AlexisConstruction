using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AlexisConstruction.Classes
{
    public class DashboardManager
    {

        public void DisplayTodaySchedule(Label lblsched)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connection.Database);

                sqlConnection.Open();

                using (SqlCommand cmd = new SqlCommand("GetScheduleToday", sqlConnection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int count = Convert.ToInt32(reader[0]);
                        lblsched.Text = count.ToString();
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            catch { throw; }
        }

        public void DisplayClients(Label lblClients)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(Connection.Database);
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand("GetClients", sqlConnection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int count = Convert.ToInt32(reader[0]);
                        lblClients.Text = count.ToString();
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            catch { throw; }
        }
        public void LoadPendingPayments(Label lblpending)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("GetPendingPayments", con);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader .Read())
                    {
                        int count = Convert.ToInt32(reader[0]);
                        lblpending.Text = count.ToString();
                    }
                    reader.Close();
                    con.Close();
                }

            }
            catch { throw; }
        }
        public void LoadCancelledRecords(Label lblcancel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetCancelledBooking", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int count = Convert.ToInt32(reader[0]);
                        lblcancel.Text = count.ToString();
                    }
                    reader.Close();
                    con.Close();
                }
            }
            catch { throw; }
        }

    }
}
