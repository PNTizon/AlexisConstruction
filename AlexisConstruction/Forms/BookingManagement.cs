using AlexisConstruction.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class BookingManagement : Form
    {
        private BookingManager bookingManager = new BookingManager();
        private bool _isPaymentComplete = false;
        public BookingManagement()
        {
            InitializeComponent();
            dtpBookingDate.MinDate = DateTime.Now;

            numHoursRendered.Value = 1;
        }

        public BookingManagement(DateTime selectedDate)
        {
            InitializeComponent();
            dtpBookingDate.MinDate = DateTime.Now;

            if (selectedDate >= DateTime.Now)
            {
                dtpBookingDate.Value = selectedDate;
            }

        }
        private void btnBook_Click(object sender, EventArgs e)
        {
            DateTime date = dtpBookingDate.Value.Date;

            if(cmbTime.SelectedItem == null)
            {
                MessageBox.Show("Please select a time.", "Time Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(!DateTime.TryParse(cmbTime.SelectedItem.ToString(), out DateTime selectedTime))
            {
                MessageBox.Show("Invalid time selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BookingDetails.BookedDate = date.Add(selectedTime.TimeOfDay);
            try
            {
                BookingDetails.ClientID = Convert.ToInt32(txtName.Text);

                int hoursRendered = (int)numHoursRendered.Value;

                if (!bookingManager.IsWorkEndTime(BookingDetails.BookedDate,hoursRendered))
                {
                    MessageBox.Show("Service would extend outside working hours or overlap lunch break.");
                    return;
                }
                if (BookingDetails.BookedDate == DateTime.MinValue)
                {
                    MessageBox.Show("Please select a valid booking date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!IsServiceSelected())
                {
                    MessageBox.Show("Please select a service to book.", "Service Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_isPaymentComplete)
                {
                    MessageBox.Show("Please complete payment before booking.", "Payment Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(!bookingManager.IsTimeSlotAvailable(BookingDetails.BookedDate, hoursRendered))
                {
                    MessageBox.Show("This time slot is already booked. Please select another time.", "Time Slot Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateAvailableTimeSlot();
                    return;
                }
                //if (bookingManager.IsDateAlreadyBooked(BookingDetails.BookedDate))
                //{
                //    MessageBox.Show("This date is already booked. Please select another date.", "Booking Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                if (!decimal.TryParse(lblTotalAmount.Text.Replace("₱", "").Trim(), out decimal totalAmount))
                {
                    MessageBox.Show("Total amount is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    return;
                }

                int bookingID = bookingManager.ScheduleBooking(BookingDetails.ClientID, BookingDetails.BookedDate, dgvServices, Convert.ToInt32(txtServiceID.Text));

                if (bookingID > 0)
                {
                    bookingManager.UpdatePaymentStatus(bookingID);

                    MessageBox.Show("Booking and payment successfully processed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Print(bookingID);
                    ClearForm();

                    UpdateAvailableTimeSlot();
                }
                else
                {
                    MessageBox.Show(!bookingManager.IsInventoryAvailable(Convert.ToInt32(txtServiceID.Text)) ? "Booking failed: Insufficient inventory for the selected service."
                        : "Failed to create booking. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BookingManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWCLIENTS' table. You can move, or remove it, as needed.
            this.sHOWCLIENTSTableAdapter.Fill(this.dataSet2.SHOWCLIENTS);

            var Client = BookingManager.LoadClients();
            cmbClients.DataSource = Client;
            cmbClients.DisplayMember = "FullName";
            cmbClients.ValueMember = "ClientID";

            var services = BookingManager.LoadServices();
            cmbServices.DataSource = services;
            cmbServices.DisplayMember = "ServiceName";
            cmbServices.ValueMember = "ServiceID";
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            try
            {
                int hoursRendered = (int)numHoursRendered.Value;
                Services services = cmbServices.SelectedItem as Services;

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
        private void Print(int bookingID)
        {
            BookingDetails bookings = new BookingDetails();
            bookings.CustomerName = cmbClients.Text;
            bookings.BookingsID = bookingID;
            bookings.BillingDate = DateTime.Now;
            bookings.MOP = "Cash";
            bookings.BookingReceipt = BookingManager.GetServiceDetails(bookings.BookingsID);

            using (bookingReceipt receipt = new bookingReceipt(bookings, bookings.BookingReceipt))
            {
                receipt.ShowDialog();
            }
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                dtpBookingDate.MinDate = DateTime.Now;

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

                _isPaymentComplete = true;

                MessageBox.Show("Payment validated! You can now book the service.", "Payment Validated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numHoursRendered_ValueChanged(object sender, EventArgs e)
        {
            UpdateAvailableTimeSlot();
        }

        private void dtpBookingDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateAvailableTimeSlot();
        }

        #region Function Helpers
        public void ClearForm()
        {
            dgvServices.Rows.Clear();

            txtCash.Text = string.Empty;
            txtChange.Text = string.Empty;
            lblTotalAmount.Text = "₱0.00";
            _isPaymentComplete = false;

            UpdateAvailableTimeSlot();
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

        private bool IsServiceSelected() =>
            dgvServices.Rows.Count > 0 && dgvServices.Rows[0].Cells["ServiceID"].Value != null;


        private void UpdateAvailableTimeSlot()
        {
            try
            {
                cmbTime.Items.Clear();

                int serviceID = 0;

                if (cmbServices.SelectedItem != null)
                {
                    Services service = cmbServices.SelectedItem as Services;
                    serviceID = service.ServiceID;

                    int hoursNeeded = (int)numHoursRendered.Value;

                    List<DateTime> availableSlots = bookingManager.GetAvailableTimeSlot(dtpBookingDate.Value, hoursNeeded);

                    foreach (DateTime slot in availableSlots)
                    {
                        cmbTime.Items.Add(slot.ToString("hh:mm tt"));
                    }

                    if (cmbTime.Items.Count > 0)
                    {
                        cmbTime.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
