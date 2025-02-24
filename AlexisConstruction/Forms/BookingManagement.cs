using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class BookingManagement : Form
    {
        private BookingManager bookingManager = new BookingManager();
        private List<BookingDetails> bookingDetails = new List<BookingDetails>();
        private List<BookingDetailsViewModel> view = new List<BookingDetailsViewModel>();
        private Display display = new Display();
        public BookingManagement()
        {
            InitializeComponent();
            LoadClients();
            LoadServices();
        }

        private void LoadClients()
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT ClientID, FirstName + ' ' + LastName AS FullName FROM Clients";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbClients.DataSource = dt;
                cmbClients.DisplayMember = "FullName";
                cmbClients.ValueMember = "ClientID";
            }
        }

        private void LoadServices()
        {
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                string query = "SELECT ServiceID, ServiceName, HourlyRate FROM Services";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbServices.DataSource = dt;
                cmbServices.DisplayMember = "ServiceName";
                cmbServices.ValueMember = "ServiceID";
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            int bookingID = Convert.ToInt32(cmbClients.SelectedValue);
            DateTime bookingDate = dtpBookingDate.Value;

            var detailsToSave = bookingDetails.Select(detail => new BookingDetails
            {
                BookingID = bookingID,
                ServiceID = detail.ServiceID,
                HoursRendered = detail.HoursRendered
            }).ToList();

            if (bookingManager.ScheduleBooking(bookingID, bookingDate, detailsToSave))
            {
                decimal totalAmount = view.Sum(detail => detail.Amount);

                if (bookingManager.GenerateBilling(bookingID, totalAmount))
                {
                    MessageBox.Show("Booking and billing successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bookingDetails.Clear();
                    UpdateTotalAmountLabel();
                }
                else
                {
                    MessageBox.Show("Booking saved, but failed to generate billing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Failed to save booking.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalAmountLabel()
        {
            decimal totalAmount = view.Sum(detail => detail.Amount);
            lblTotalAmount.Text = $"{totalAmount:C}";
        }

        private void BookingManagement_Load(object sender, EventArgs e)
        {

        }

       

        private void btnAddService_Click(object sender, EventArgs e)
        {
            int serviceID = Convert.ToInt32(cmbServices.SelectedValue);
            string serviceName = cmbServices.Text;
            int hoursRendered = (int)nudHoursRendered.Value;

            decimal hourlyRate = ((DataTable)cmbServices.DataSource)
                .AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("ServiceID") == serviceID)?
                .Field<decimal>("HourlyRate") ?? 0;

            var detail = new BookingDetailsViewModel
            {
                ServiceID = serviceID,
                HoursRendered = hoursRendered,
                Service = new Services { ServiceID = serviceID, HourlyRate = hourlyRate }
            };

            view.Add(detail);

            dgvServices.DataSource = null;
            dgvServices.DataSource = view;

            UpdateTotalAmountLabel();
        }
    }
}
