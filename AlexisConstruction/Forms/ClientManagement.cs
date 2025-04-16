using AlexisConstruction.Classes;
using System;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class ClientManagement : Form
    {
        private ClientManager userManagement = new ClientManager();
        private Helper helper = new Helper();
        private DataGridSelection select = new DataGridSelection();
        private bool isEditMode = false;
        private Dashboard dashboard = new Dashboard();
        public ClientManagement()
        {
            InitializeComponent();
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                if(isEditMode)
                {
                    MessageBox.Show("Finish editing the current record before adding a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Client client = new Client
                {
                    FirstName = txtFirstName.Text,
                    Lastname = txtLastname.Text,
                    CountryCode = txtCountyCode.Text,
                    ContactNumber = txtContactNumber.Text,
                    Email = txtemail.Text,
                    Address = txtaddress.Text
                };
                if (string.IsNullOrWhiteSpace(client.FirstName) || string.IsNullOrWhiteSpace(client.Lastname) || string.IsNullOrWhiteSpace(client.ContactNumber) ||
                      string.IsNullOrWhiteSpace(client.Email) || string.IsNullOrWhiteSpace(client.Address))
                {
                    MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (userManagement.AddClient(client))
                {
                    MessageBox.Show("Client added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //display.GetClients(dgvClients);
                    this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);
                    Clear();

                    dashboard.LoadClients();
                }
                else
                {
                    MessageBox.Show("Failed to add client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateClient_Click(Object sender, EventArgs e)
        {
            try
            {
                if (dgvClients.SelectedRows.Count > 0)
                {
                    int clientId = Convert.ToInt32(dgvClients.SelectedRows[0].Cells["ClientID"].Value);

                    Client client = new Client
                    {
                        ClientID = clientId,
                        FirstName = txtFirstName.Text,
                        Lastname = txtLastname.Text,
                        CountryCode = txtCountyCode.Text,
                        ContactNumber = txtContactNumber.Text,
                        Email = txtemail.Text,
                        Address = txtaddress.Text
                    };

                    if (userManagement.UpdateClient(client))
                    {
                        MessageBox.Show("Client information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //display.GetClients(dgvClients);
                        this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);
                        isEditMode = false;
                        Clear();

                        dashboard.LoadClients();
                    }
                   
                    else
                    {
                        MessageBox.Show("Failed to update client's information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a client to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            if(isEditMode)
            {
                MessageBox.Show("Cannot delete while editing a record. Please finish editing first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvClients.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvClients.SelectedRows[0];
                int clientID = Convert.ToInt32(selectedRow.Cells["ClientID"].Value);

                var confirm = MessageBox.Show("Are you sure you want to delete this client?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    if (userManagement.DeleteClient(clientID))
                    {
                        MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);
                        Clear();

                        dashboard.LoadClients();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete this client because it is assosciated with one or more booking.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a client to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClientManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWCLIENTS' table. You can move, or remove it, as needed.
            this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);
            PhoneNumberList.ListPhneNumber(CountryCodecmb_SelectedIndexChanged, CountryCodecmb);
        }

        private void CountryCodecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PhoneNumberList.ComboBox_autoModifier(CountryCodecmb_SelectedIndexChanged, CountryCodecmb, txtCountyCode);
        }

        private void txtContactNumber_TextChanged(object sender, EventArgs e)
        {
            helper.HelperNumberRestriction(txtContactNumber);
            helper.isValidPhoneNumber(txtCountyCode, txtContactNumber);
        }

        public void Clear()
        {
            txtFirstName.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtCountyCode.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtemail.Text = string.Empty;
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            Clear();
        }

        private void dgvClients_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isEditMode = false;
            Clear();
        }

        private void dgvClients_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                select.PopulateClientData(e.RowIndex, dgvClients, txtFirstName, txtLastname, txtCountyCode, txtContactNumber, txtaddress, txtemail);
                isEditMode = true;
            }
        }
    }
}
