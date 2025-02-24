using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisConstruction.Classes
{
    public class ClientManager
    {
        public bool AddClient(Client client)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = "INSERT INTO Clients (Firstname,Lastname,CountryCode,ContactNumber,Email,Address) VALUES (@firstname,@lastname,@countrycode,@contactnumber,@email,@address)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
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
        public bool UpdateClient(Client client)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = "UPDATE Clients SET FirstName = @firstname , LastName = @lastname , CountryCode= @countrycode,ContactNumber = @contactnumber, Email = @email, Address = @address WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
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
        public bool DeleteClient(int serviceID)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = "DELETE FROM Clients WHERE ClientID = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", serviceID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public Client GetClientByID(int clientID)
        {
            Client client = null;
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = "SELECT * FROM Clients WHERE ClientID = @ClientID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ClientID", clientID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                ClientID = Convert.ToInt32(reader["ClientID"]),
                                FirstName = reader["FirstName"].ToString(),
                                Lastname = reader["LastName"].ToString(),
                                CountryCode = reader["CountryCode"].ToString(),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                Email = reader["Email"].ToString(),
                                Address = reader["Address"].ToString()
                            };
                        }
                    }
                }
            }
            return client;
        }
    }
}
