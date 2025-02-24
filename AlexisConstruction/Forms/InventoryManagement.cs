using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadInventoryItems();
        }

        private void LoadInventoryItems()
        {
            dgvInventory.DataSource = inventorymanange.GetInventoryItems();
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            Inventory item = new Inventory
            {
                ItemName = txtItemName.Text,
                Quantity = int.Parse(txtQuantity.Text)
            };

            if (inventorymanange.AddInventoryItem(item))
            {
                MessageBox.Show("Inventory item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInventoryItems();
            }
            else
            {
                MessageBox.Show("Failed to add inventory item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var selectedRow = dgvInventory.SelectedRows[0];
                Inventory item = new Inventory
                {
                    InventoryID = (int)selectedRow.Cells["InventoryID"].Value,
                    ItemName = txtItemName.Text,
                    Quantity = int.Parse(txtQuantity.Text)
                };

                if (inventorymanange.UpdateInventoryItem(item))
                {
                    MessageBox.Show("Inventory item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryItems();
                }
                else
                {
                    MessageBox.Show("Failed to update inventory item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an inventory item to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnDeleteInventory_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var selectedRow = dgvInventory.SelectedRows[0];
                int inventoryID = (int)selectedRow.Cells["InventoryID"].Value;

                if (inventorymanange.DeleteInventoryItem(inventoryID))
                {
                    MessageBox.Show("Inventory item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventoryItems();
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
            gridselection.PopulateInventoryDate(e.RowIndex, dgvInventory, txtItemName, txtQuantity);
        }
    }
}
