using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisConstruction.Classes
{
    public class ClientManager
    {
        public bool AddClient(Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertNewClient", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstname", client.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", client.Lastname);
                        cmd.Parameters.AddWithValue("@countrycode", client.CountryCode);
                        cmd.Parameters.AddWithValue("@contactnumber", client.ContactNumber);
                        cmd.Parameters.AddWithValue("@email", client.Email);
                        cmd.Parameters.AddWithValue("@address", client.Address);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch { throw; }
        }
        public bool UpdateClient(Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateClient", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClientID", client.ClientID);
                        cmd.Parameters.AddWithValue("@firstname", client.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", client.Lastname);
                        cmd.Parameters.AddWithValue("@countrycode", client.CountryCode);
                        cmd.Parameters.AddWithValue("@contactnumber", client.ContactNumber);
                        cmd.Parameters.AddWithValue("@email", client.Email);
                        cmd.Parameters.AddWithValue("@address", client.Address);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch { throw; }
           
        }
        public bool DeleteClient(int serviceID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteClient", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", serviceID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch { throw; }
        }
    }
}
