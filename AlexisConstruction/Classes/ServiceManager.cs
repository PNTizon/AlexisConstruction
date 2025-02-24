using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexisConstruction.Classes
{
    public  class ServiceManager
    {

        public static string AddService(Services service)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    string query = "INSERT INTO Services (ServiceName, HourlyRate) VALUES (@service, @rate)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@service", service.ServiceName);
                        cmd.Parameters.AddWithValue("@rate", service.HourlyRate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "Service added successfully";
                        }
                        else
                        {
                            return "Error: Failed to add the service";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
        public bool EditService(Services service)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "UPDATE Services SET  ServiceName = @name, HourlyRate =@rate WHERE ServiceID= @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", service.ServiceID);
                    cmd.Parameters.AddWithValue("@name", service.ServiceName);
                    cmd.Parameters.AddWithValue("@rate", service.HourlyRate);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool DeleteService(int service)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "DELETE FROM Services WHERE ServiceID = @id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", service);

                    return cmd.ExecuteNonQuery() > 0;

                }
            }
        }
        public bool AddInventoryItem(Inventory item)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "INSERT INTO Inventory (ItemName, Quantity) VALUES (@itemName, @quantity)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@itemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Inventory> GetInventoryItems()
        {
            List<Inventory> inventoryList = new List<Inventory>();
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT InventoryID, ItemName, Quantity FROM Inventory";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inventoryList.Add(new Inventory
                            {
                                InventoryID = (int)reader["InventoryID"],
                                ItemName = reader["ItemName"].ToString(),
                                Quantity = (int)reader["Quantity"]
                            });
                        }
                    }
                }
            }
            return inventoryList;
        }

        public bool UpdateInventoryItem(Inventory item)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "UPDATE Inventory SET ItemName = @itemName, Quantity = @quantity WHERE InventoryID = @inventoryID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@itemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    cmd.Parameters.AddWithValue("@inventoryID", item.InventoryID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteInventoryItem(int inventoryID)
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "DELETE FROM Inventory WHERE InventoryID = @inventoryID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@inventoryID", inventoryID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
