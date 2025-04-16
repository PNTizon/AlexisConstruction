using AlexisConstruction.Classes;
using ServiceStack;
using System;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class InventoryManagement : Form
    {
        private ServiceManager inventorymanange = new ServiceManager();
        private DataGridSelection gridselection = new DataGridSelection();
        private bool IsEditMode = false;
        public InventoryManagement()
        {
            InitializeComponent();
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            try
            {
                int serviceID = Convert.ToInt32(cmbServices.SelectedValue);
                string itemname = txtItemName.Text.Trim();
                int quantity = Convert.ToInt32(txtQuantity.Text);
                string servicename = cmbServices.Text;

                if (IsEditMode)
                {
                    MessageBox.Show("Finish editing the current record before adding a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbServices.SelectedValue == null || string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtQuantity.Text))
                {
                    MessageBox.Show("Please fill in all the fields.");
                    return;
                }
                if(inventorymanange.CheckDuplication(itemname,dgvInventory))
                {
                    MessageBox.Show("Item already exists in the selected service.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (inventorymanange.AddItemToService(serviceID, itemname, quantity, servicename))
                {
                    MessageBox.Show("Inventory item added successfully!", "Success");
                    inventorymanange.LoadServiceItems(serviceID, dgvInventory);
                    this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
                    //inventorymanange.LoadServices(cmbServices);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Failed to add inventory item.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    if (int.TryParse(dgvInventory.SelectedRows[0].Cells["InventoryID"].Value.ToString(), out int inventoryID))
                    {
                        string itemName = txtItemName.Text.Trim();

                        if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                        {
                            MessageBox.Show("Please enter a valid quantity.");
                            return;
                        }

                        Inventory itemToUpdate = new Inventory
                        {
                            InventoryID = inventoryID,
                            ItemName = txtItemName.Text,
                            Quantity = quantity
                        };

                        if (inventorymanange.UpdateInventoryItem(itemToUpdate))
                        {
                            MessageBox.Show("Inventory item updated successfully!", "Success");
                            inventorymanange.LoadServiceItems(Convert.ToInt32(cmbServices.SelectedValue), dgvInventory);
                            //this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
                            inventorymanange.LoadServices(cmbServices);
                            IsEditMode = false;
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update inventory item.", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Inventory ID.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select an inventory item to update.", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            if(IsEditMode)
            {
                MessageBox.Show("Finish editing the current record before deleting.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int inventoryID = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["InventoryID"].Value);

                var result = MessageBox.Show("Are you sure you want to delete this inventory item?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (inventorymanange.DeleteInventoryItem(inventoryID))
                    {
                        MessageBox.Show("Inventory item deleted successfully!");
                        //inventorymanange.LoadServiceItems(serviceID, dgvInventory);

                        inventorymanange.LoadServices(cmbServices);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete inventory item.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item to delete.");
            }
        }
        public void Clear()
        {
            txtItemName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            cmbServices.SelectedValue = -1;
        }
        
        private void InventoryManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWINVENTORY' table. You can move, or remove it, as needed.
            this.sHOWINVENTORYTableAdapter.Fill(this.dataSet2.SHOWINVENTORY);
            inventorymanange.LoadServices(cmbServices);
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue != null && int.TryParse(cmbServices.SelectedValue.ToString(), out int serviceID))
            {
                inventorymanange.LoadServiceItems(serviceID, dgvInventory);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IsEditMode = false;
            Clear();
        }

        private void dgvInventory_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IsEditMode = false;
            Clear();
        }

        private void dgvInventory_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridselection.PopulateInventoryDate(e.RowIndex, dgvInventory, txtItemName, txtQuantity, cmbServices);
        }
    }
}
