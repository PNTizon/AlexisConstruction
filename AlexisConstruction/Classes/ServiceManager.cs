﻿using System;
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

                    using (SqlCommand cmd = new SqlCommand("InsertServices", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateServices", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", service.ServiceID);
                        cmd.Parameters.AddWithValue("@name", service.ServiceName);
                        cmd.Parameters.AddWithValue("@rate", service.HourlyRate);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
           catch { throw; }
        }
        public bool DeleteService(int service)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteServices", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", service);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch { throw; }
           
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
                    using (SqlCommand cmd = new SqlCommand("UpdateInventory", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
                   
                    using (SqlCommand cmd = new SqlCommand("DeleteInventory", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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

                   
                    int inventoryID;
                    using (SqlCommand cmd = new SqlCommand("InsertItem", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@itemname", itemname);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            inventoryID = Convert.ToInt32(result);
                        }
                        else
                        {
                            using (SqlCommand insertcmd = new SqlCommand("InsertInventory", con))
                            {
                                insertcmd.CommandType = CommandType.StoredProcedure;
                                insertcmd.Parameters.AddWithValue("@itemname", itemname);
                                insertcmd.Parameters.AddWithValue("@quantity", quantity);
                                insertcmd.Parameters.AddWithValue("@servicename", servicename);
                                inventoryID = (int)insertcmd.ExecuteScalar();
                            }
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("InsertServiceDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
                 
                    using (SqlCommand cmd = new SqlCommand("LoadServiceItems", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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

