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
    public partial class ServiceManagement : Form
    {
        private ServiceManager services = new ServiceManager();
        private Helper helper = new Helper();
        private Display display = new Display();
        private DataGridSelection select = new DataGridSelection(); 
        public ServiceManagement()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

            string result = ServiceManager.AddService(newService);

            if (result == "Service added successfully")
            {
                MessageBox.Show("Service added successfully!");
                this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
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

        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            select.PopulateService(e.RowIndex,dgvServices,txtHourlyRate,txtServiceName);
        }
        private void ServicesManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWSERVICES' table. You can move, or remove it, as needed.
            this.sHOWSERVICESTableAdapter.Fill(this.dataSet2.SHOWSERVICES);
        }
    }
}
