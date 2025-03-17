using AlexisConstruction.Classes;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class BookingManagement : Form
    {
        private BookingManager bookingManager = new BookingManager();
        public BookingManagement()
        {
            InitializeComponent();
            dtpBookingDate.MinDate = DateTime.Now;

            cmbClients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbClients.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbServices.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbServices.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private static List<Client> LoadClients()
        {
            List<Client> customer = new List<Client>();

            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ConcatinateName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customer.Add(new Client
                        {
                            ClientID = Convert.ToInt32(reader["ClientID"].ToString()),
                            Fullname = reader["Fullname"].ToString(),
                        });
                    }
                }
            }
            return customer;
        }

        public static List<Services> LoadServices()
        {
            List<Services> service = new List<Services>();
            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ServiceID, ServiceName, HourlyRate FROM Services", con);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        service.Add(new Services
                        {
                            ServiceID = Convert.ToInt32(reader["ServiceID"].ToString()),
                            ServiceName = reader["ServiceName"].ToString(),
                            HourlyRate = Convert.ToDecimal(reader["HourlyRate"].ToString()),
                        });
                    }
                }
                return service;
            }
        }
        private void btnBook_Click(object sender, EventArgs e)
        {
            BookingDetails.ClientID = Convert.ToInt32(txtName.Text);
            BookingDetails.BookedDate = dtpBookingDate.Value;

            if (!button1.Enabled)
            {
                MessageBox.Show("Please complete payment before booking.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int clientID = Convert.ToInt32(cmbClients.SelectedValue);
            DateTime bookedDate = dtpBookingDate.Value;

            if (bookedDate == DateTime.MinValue)
            {
                MessageBox.Show("Please select a valid booking date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (bookingManager.IsDateAlreadyBooked(dtpBookingDate.Value))
            {
                MessageBox.Show("This date is already booked. Please select another date.", "Booking Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string totalText = lblTotalAmount.Text.Replace("₱", "").Trim();
            if (!decimal.TryParse(totalText, out decimal totalAmount))
            {
                MessageBox.Show("Total amount is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int bookingID = bookingManager.ScheduleBooking(clientID, bookedDate, dgvServices, Convert.ToInt32(txtServiceID.Text));

            if (bookingID > 0)
            {
                bookingManager.UpdatePaymentStatus(bookingID);

                MessageBox.Show("Booking and payment successfully processed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button3_Click();
                button1.Enabled = false;
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to create booking. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dgvServices.Rows)
            {
                if (row.Cells["Amount"].Value != null)
                {
                    totalAmount += Convert.ToDecimal(row.Cells["Amount"].Value);
                }
            }
            lblTotalAmount.Text = $"{totalAmount:C}";
        }

        private void BookingManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWCLIENTS' table. You can move, or remove it, as needed.
            this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);
            var Client = LoadClients();
            cmbClients.DataSource = Client;
            cmbClients.DisplayMember = "FullName";
            cmbClients.ValueMember = "ClientID";

            var Services = LoadServices();
            cmbServices.DataSource = Services;
            cmbServices.DisplayMember = "ServiceName";
            cmbServices.ValueMember = "ServiceID";
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            int hoursRendered = (int)nudHoursRendered.Value;
            Services services = cmbServices.SelectedItem as Services;
            try
            {
                if (services != null)
                {
                    bool Serviceexist = false;
                    foreach (DataGridViewRow row in dgvServices.Rows)
                    {
                        if (row.Cells["ServiceID"].Value.ToString() == services.ServiceID.ToString())
                        {
                            int currentHoursRendered = Convert.ToInt32(row.Cells["HoursRendered"].Value);
                            int updatedHours = currentHoursRendered + hoursRendered;
                            row.Cells["HoursRendered"].Value = updatedHours;

                            decimal hourlyRate = Convert.ToDecimal(row.Cells["HourlyRate"].Value);
                            row.Cells["Amount"].Value = updatedHours * hourlyRate;

                            Serviceexist = true;
                            break;
                        }
                    }

                    if (!Serviceexist)
                    {
                        int rowIndex = dgvServices.Rows.Add();
                        DataGridViewRow row = dgvServices.Rows[rowIndex];
                        row.Cells["ServiceName"].Value = services.ServiceName;
                        row.Cells["ServiceID"].Value = services.ServiceID;
                        row.Cells["HoursRendered"].Value = hoursRendered;
                        row.Cells["HourlyRate"].Value = services.HourlyRate;
                        row.Cells["Amount"].Value = hoursRendered * services.HourlyRate;
                    }
                }
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dgvServices.Columns.Contains("Service"))
                dgvServices.Columns["Service"].Visible = false;
        }
        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbClients != null)
            {
                Client customer = cmbClients.SelectedItem as Client;
                txtName.Text = customer.ClientID.ToString();
            }
        }

        private void cmbServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServices.SelectedValue != null)
            {
                Services customer = cmbServices.SelectedItem as Services;
                txtServiceID.Text = customer.ServiceID.ToString();
            }
        }

        private void button3_Click()
        {
            BookingDetails bookings = new BookingDetails();
            int bookingID = Convert.ToInt32(cmbClients.SelectedValue);
            bookings.CustomerName = cmbClients.Text;
            bookings.BookingsID = Convert.ToInt32(txtName.Text);
            bookings.BillingDate = DateTime.Now;
            bookings.MOP = "Cash";
            bookings.BookingReceipt = GetServiceDetails(bookings.BookingsID);

            using (bookingReceipt receipt = new bookingReceipt(bookings, bookings.BookingReceipt))
            {
                receipt.ShowDialog();
            }
        }
        private List<Orders> GetServiceDetails(int bookingID)
        {
            List<Orders> services = new List<Orders>();

            using (SqlConnection con = new SqlConnection(Connection.Database))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("GetServiceDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bookingID", bookingID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new Orders
                        {
                            ServiceName = reader["ServiceName"].ToString(),
                            HourlyRate = Convert.ToInt32(reader["HourlyRate"]),
                            HoursRendered = Convert.ToInt32(reader["HoursRendered"]),
                        });
                    }
                }
            }
            return services;
        }
        private void CalculateChange()
        {
            decimal cash = 0;
            decimal change = 0;

            string totalText = lblTotalAmount.Text.Replace("₱", "").Trim();
            if (decimal.TryParse(totalText, out decimal totalAmount))
            {
                if (decimal.TryParse(txtCash.Text, out cash))
                {
                    change = cash - totalAmount;
                    txtChange.Text = change.ToString("0.00");
                }
                else
                {
                    txtChange.Text = "0.00";
                }
            }
        }
        public void ClearForm()
        {
            dgvServices.Rows.Clear();

            txtCash.Text = "";
            txtChange.Text = "";
            lblTotalAmount.Text = "₱0.00";
        }
        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            dtpBookingDate.MinDate = DateTime.Now.AddHours(1);

            if (!decimal.TryParse(txtCash.Text, out decimal cash))
            {
                MessageBox.Show("Please enter a valid cash amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(lblTotalAmount.Text.Replace("₱", "").Trim(), NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal totalAmount))
            {
                MessageBox.Show("Total amount is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cash < totalAmount)
            {
                MessageBox.Show("Insufficient cash entered.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            button1.Enabled = true;

            MessageBox.Show("Payment validated! You can now book the service.", "Payment Validated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtServices_TextChanged(object sender, EventArgs e)
        {
            LoadServices();
        }

        private void txtClients_TextChanged(object sender, EventArgs e)
        {
            LoadClients();
        }
    }
}
