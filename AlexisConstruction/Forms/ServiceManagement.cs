using AlexisConstruction.Classes;
using System;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class ServiceManagement : Form
    {
        private ServiceManager services = new ServiceManager();
        private DataGridSelection select = new DataGridSelection();
        private bool isEditMode = false;
        public ServiceManagement()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(isEditMode)
                {
                    MessageBox.Show("Finish editing the current record before adding a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtServiceName.Text) || !decimal.TryParse(txtHourlyRate.Text, out decimal hourlyRate))
                {
                    MessageBox.Show("Please enter valid service name and hourly rate.");
                    return;
                }
                if(services.CheckServiceDuplication(txtServiceName.Text,dgvServices))
                {
                    MessageBox.Show("Service already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Services newService = new Services
                {
                    ServiceName = txtServiceName.Text,
                    HourlyRate = hourlyRate,
                };

                if (services.AddService(newService))
                {
                    MessageBox.Show("Service added successfully!");
                    this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Failed to add service.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvServices.SelectedRows.Count > 0)
                {
                    Services service = new Services
                    {
                        ServiceID = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value),
                        ServiceName = txtServiceName.Text,
                        HourlyRate = decimal.Parse(txtHourlyRate.Text)
                    };

                    if (services.EditService(service))
                    {
                        MessageBox.Show("Service updated successfully!");
                        this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
                        isEditMode = false;
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update service.");
                    }
                }
                else
                {
                    MessageBox.Show("Select a service to edit.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                MessageBox.Show("Cannot delete while editing a record. Please finish editing first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvServices.SelectedRows.Count > 0)
            {
                int serviceID = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);

                var confirmDelete = MessageBox.Show("Are you sure you want to delete this service?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmDelete == DialogResult.Yes)
                {
                    try
                    {
                        if (services.DeleteService(serviceID))
                        {
                            MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
                            Clear();
                        }
                        else
                        {
                            MessageBox.Show("Cannot delete this service because it is assosciated with one or more booking.", "Operation Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "An error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a service to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Clear()
        {
            txtServiceName.Text = string.Empty;
            txtHourlyRate.Text = string.Empty;
        }
        
        private void ServicesManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWSERVICES' table. You can move, or remove it, as needed.
            this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            Clear();
        }

        private void dgvServices_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isEditMode = false;
            Clear();
        }

        private void dgvServices_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            select.PopulateService(e.RowIndex, dgvServices, txtHourlyRate, txtServiceName);
            isEditMode = true;
        }
    }
}
