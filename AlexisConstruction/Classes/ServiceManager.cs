using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class ServiceManager
    {
        #region Services
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
        #endregion

        #region Inventory
        public bool UpdateInventoryItem(Inventory item)
        {
            try
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
            catch { throw; }
        }

        public bool DeleteInventoryItem(int inventoryID)
        {
            try
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
            catch { throw; }
        }

        public bool AddItemToService(int serviceID, string itemname, int quantity,string servicename)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    string checkitenQuantity = "SELECT InventoryID FROM Inventory WHERE ItemName = @itemname";
                    int inventoryID;
                    using (SqlCommand cmd = new SqlCommand(checkitenQuantity, con))
                    {
                        cmd.Parameters.AddWithValue("@itemname", itemname);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryID = Convert.ToInt32(result);
                        }
                        else
                        {
                            string insetediten = "INSERT INTO Inventory(ServiceName,ItemName,Quantity) OUTPUT INSERTED.InventoryID VALUES (@servicename,@itemname,@quantity)";
                            using (SqlCommand insertcmd = new SqlCommand(insetediten, con))
                            {
                                insertcmd.Parameters.AddWithValue("@itemname", itemname);
                                insertcmd.Parameters.AddWithValue("@quantity", quantity);
                                insertcmd.Parameters.AddWithValue("@servicename", servicename);
                                inventoryID = (int)insertcmd.ExecuteScalar();
                            }
                        }
                    }
                    string serviceMaterial = "INSERT INTO ServiceMaterials (ServiceID,InventoryID) VALUES (@serviceID,@inventoryID)";
                    using (SqlCommand cmd = new SqlCommand(serviceMaterial, con))
                    {
                        cmd.Parameters.AddWithValue("@serviceID", serviceID);
                        cmd.Parameters.AddWithValue("@inventoryID", inventoryID);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        public bool LoadServiceItems(int serviceID, DataGridView inventory)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    string query = @"SELECT i.InventoryID ,i.ServiceName,i.ItemName, i.Quantity
                                    FROM ServiceMaterials sm 
                                    INNER JOIN Inventory i on sm.InventoryID = i.InventoryID 
                                    WHERE sm.ServiceID = @serviceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@serviceID", serviceID);
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        inventory.DataSource = dt;
                    }
                }
                return true;
            }
            catch { throw; }
        }

        #endregion
    }
}

