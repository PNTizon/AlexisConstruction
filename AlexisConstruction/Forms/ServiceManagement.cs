using AlexisConstruction.Classes;
using System;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class ServiceManagement : Form
    {
        private ServiceManager services = new ServiceManager();
        private DataGridSelection select = new DataGridSelection();
        public ServiceManagement()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtServiceName.Text) || !decimal.TryParse(txtHourlyRate.Text, out decimal hourlyRate))
                {
                    MessageBox.Show("Please enter valid service name and hourly rate.");
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
                MessageBox.Show(ex.Message, "Ann error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Ann error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count > 0)
            {
                int serviceID = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);

                DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete this service?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmDelete == DialogResult.Yes)
                {
                    if (services.DeleteService(serviceID))
                    {
                        MessageBox.Show("Service deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete service.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            select.PopulateService(e.RowIndex, dgvServices, txtHourlyRate, txtServiceName);
        }
        private void ServicesManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWSERVICES' table. You can move, or remove it, as needed.
            this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
        }
    }
}
