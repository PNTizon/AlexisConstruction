using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class InventoryManagement : Form
    {
        private ServiceManager inventorymanange = new ServiceManager();
        private DataGridSelection gridselection = new DataGridSelection();
        private Display display = new Display();
        public InventoryManagement()
        {
            InitializeComponent();
        }

        private void LoadServices()
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT ServiceID, ServiceName FROM Services";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    cmbServices.DataSource = dt;
                    cmbServices.DisplayMember = "ServiceName";
                    cmbServices.ValueMember = "ServiceID";
                }
            }
        }
        public void LoadServiceItems (int serviceID,DataGridView grid)
        {
            using(SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                string query = @"SELECT InventoryID,ItemName,Quantity FROM Inventory WHERE ServiceID = @servicename";

                using(SqlCommand cmd = new SqlCommand(query,con))
                {
                    cmd.Parameters.AddWithValue("@servicename", serviceID);
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    grid.DataSource = dt;
                }
            }
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue == null || string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtQuantity.Text))
            {
                MessageBox.Show("Please fill in all the fields.");
                return;
            }
            int serviceID = Convert.ToInt32(cmbServices.SelectedValue);
            string itemname = txtItemName.Text.Trim();
            int quantity = Convert.ToInt32(txtQuantity.Text);
            string servicename = cmbServices.Text;
            if (inventorymanange.AddItemToService(serviceID,itemname,quantity,servicename))
            {
                MessageBox.Show("Inventory item added successfully!", "Success");
                inventorymanange.LoadServiceItems(serviceID,dgvInventory);
                //display.GetAllInventory(dgvInventory);
            }
            else
            {
                MessageBox.Show("Failed to add inventory item.", "Error");
            }
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var selectedRow = dgvInventory.SelectedRows[0];
                int inventoryID = Convert.ToInt32(selectedRow.Cells["InventoryID"].Value);
                string itemName = txtItemName.Text.Trim();

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.");
                    return;
                }

                Inventory itemToUpdate = new Inventory
                {
                    InventoryID = inventoryID,
                    ItemName = itemName,
                    Quantity = quantity
                };

                if (inventorymanange.UpdateInventoryItem(itemToUpdate))
                {
                    MessageBox.Show("Inventory item updated successfully!", "Success");
                    inventorymanange.LoadServiceItems(Convert.ToInt32(cmbServices.SelectedValue), dgvInventory);
                    //display.GetAllInventory(dgvInventory);
                }
                else
                {
                    MessageBox.Show("Failed to update inventory item.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item to update.", "Warning");
            }
        }

        private void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var selectedRow = dgvInventory.SelectedRows[0];
                int inventoryID = (int)selectedRow.Cells["InventoryID"].Value;
                int serviceID = Convert.ToInt32(cmbServices.SelectedValue);

                if (inventorymanange.DeleteInventoryItem(inventoryID))
                {
                    MessageBox.Show("Inventory item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inventorymanange.LoadServiceItems(serviceID, dgvInventory);
                    //display.GetAllInventory(dgvInventory);
                }
                else
                {
                    MessageBox.Show("Failed to delete inventory item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gridselection.PopulateInventoryDate(e.RowIndex, dgvInventory, txtItemName, txtQuantity,cmbServices);
        }

        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            LoadServices();
            display.GetAllInventory(dgvInventory);

            cmbServices.SelectedIndexChanged += cmbServices_SelectedIndexChanged;
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbServices.SelectedValue != null && int.TryParse(cmbServices.SelectedValue.ToString(),out int serviceID))
            {
                inventorymanange.LoadServiceItems(serviceID, dgvInventory);
            }
        }
    }
}
