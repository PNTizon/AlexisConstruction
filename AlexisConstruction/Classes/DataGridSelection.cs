using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexisConstruction.Classes
{
    public class DataGridSelection
    {
        public void PopulateClientData(int rowIndex, DataGridView dataGrid, params TextBox[] textboxes)
        {
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGrid.Rows[rowIndex];

                string GetValue(string columnName) => row.Cells[columnName].Value?.ToString() ?? string.Empty;

                if (textboxes.Length >= 6)
                {
                    textboxes[0].Text = GetValue("FirstName");
                    textboxes[1].Text = GetValue("Lastname");
                    textboxes[2].Text = GetValue("CountryCode");
                    textboxes[3].Text = GetValue("ContactNumber");
                    textboxes[4].Text = GetValue("Address");
                    textboxes[5].Text = GetValue("Email");
                }
                int selectedID = Convert.ToInt32(row.Cells["ClientID"].Value);
            }
        }
        public void PopulateInventoryDate(int rowIndex, DataGridView inventoryView, TextBox itename, TextBox quantity)
        {
            try
            {
                if (rowIndex >= 0)
                {
                    DataGridViewRow row = inventoryView.Rows[rowIndex];

                    string GetValue(string columnName) => row.Cells[columnName].Value?.ToString() ?? string.Empty;
                    itename.Text = GetValue("ItemName");
                    quantity.Text = GetValue("Quantity");

                    int selectedID = Convert.ToInt32(row.Cells["InventoryID"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while selecting the row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
