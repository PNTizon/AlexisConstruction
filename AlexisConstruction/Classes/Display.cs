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
    public class Display
    {
        public DataTable GetAllServices()
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Services", con))
                {
                    using (SqlDataAdapter reader = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        reader.Fill(dt);

                        return dt;
                    }
                }
            }
        }
        public void GetClients(DataGridView grid)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT ClientID, FirstName, LastName, CountryCode,ContactNumber, Email, Address FROM Clients";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    da.Fill(table);

                    grid.DataSource = table;

                }
            }
        }


    }
}
