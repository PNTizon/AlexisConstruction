using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class Display
    {
        public void LoadWeeklySchedule(DataGridView grid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("WEEKLYSCHEDULE", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grid.DataSource = dt;

                    if (grid.Columns["ServiceID"] != null)
                        grid.Columns["ServiceID"].Visible = false;
                    Dictionary<int, DataTable> toolsData = new Dictionary<int, DataTable>();

                    foreach (DataRow row in dt.Rows)
                    {
                        int serviceID = Convert.ToInt32(row["ServiceID"]);
                        DateTime estimatedEndTime = Convert.ToDateTime(row["EstimatedEndTime"]);

                        if (!toolsData.ContainsKey(serviceID))
                        {
                            toolsData[serviceID] = LoadAssociatedTools(serviceID, estimatedEndTime);
                        }
                    }
                }
            }
            catch { throw; }
        }

        public DataTable LoadAssociatedTools(int serviceID, DateTime endTime)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    if(endTime <= DateTime.Now)
                    {
                        SqlCommand cmd = new SqlCommand("LoadAssociatedTools", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@serviceID", serviceID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                    else
                    {
                        return new DataTable();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void CheckAndUpdateCompletedServices(DataGridView grid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.Database))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("UpdateIventoryAndBooking", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0 && grid != null)
                    {
                        LoadWeeklySchedule(grid);
                    }
                }

            }
            catch { throw; }
        }
    }
}
