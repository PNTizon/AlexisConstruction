using AlexisConstruction.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class InventoryManagement : Form
    {
        private ServiceManager inventorymanange = new ServiceManager();
        private DataGridSelection gridselection = new DataGridSelection();
        public InventoryManagement()
        {
            InitializeComponent();

            //cmbServices.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cmbServices.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadServices()
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                
                using (SqlCommand cmd = new SqlCommand("LoadServices", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());

                    cmbServices.DataSource = dt;
                    cmbServices.DisplayMember = "ServiceName";
                    cmbServices.ValueMember = "ServiceID";
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
            if (inventorymanange.AddItemToService(serviceID, itemname, quantity, servicename))
            {
                MessageBox.Show("Inventory item added successfully!", "Success");
                inventorymanange.LoadServiceItems(serviceID, dgvInventory);
                this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
                Clear();

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
                int inventoryID = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["InventoryID"].Value);
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
                    this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
                    Clear();
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
                int inventoryID = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["InventoryID"].Value);
                int serviceID = Convert.ToInt32(cmbServices.SelectedValue);

                if (inventorymanange.DeleteInventoryItem(inventoryID))
                {
                    MessageBox.Show("Inventory item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inventorymanange.LoadServiceItems(serviceID, dgvInventory);
                    this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
                    Clear();
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
        public void Clear()
        {
            txtItemName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            cmbServices.SelectedValue = -1;
        }
        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gridselection.PopulateInventoryDate(e.RowIndex, dgvInventory, txtItemName, txtQuantity, cmbServices);
        }

        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWINVENTORY' table. You can move, or remove it, as needed.
            this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
            LoadServices();

            cmbServices.SelectedIndexChanged += cmbServices_SelectedIndexChanged;
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue != null && int.TryParse(cmbServices.SelectedValue.ToString(), out int serviceID))
            {
                inventorymanange.LoadServiceItems(serviceID, dgvInventory);
            }
        }
    }
}
