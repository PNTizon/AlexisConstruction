using AlexisConstruction.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class EditEvent : Form
    {
        private EventManager manager = new EventManager();
        private DateTime selectedDate;
        private Display display = new Display();

        public EditEvent(int days, DateTime selectedDate)
        {
            InitializeComponent();
            this.selectedDate = new DateTime(Calendar.staticYear, Calendar.staticMonth, days);
            LoadRecords();

            dtpTime.Value = DateTime.Today.AddHours(8);

            if (selectedDate >= DateTime.Now)
            {
                dtpBookedDate.Value = selectedDate;
            }
        }
     
        private void EventEditor_Load(object sender, EventArgs e)
        {
            try
            {
                this.editEventsTableAdapter.Fill(this.dataSet2.EditEvents);
            }

            catch (ConstraintException ex)
            {
                Console.WriteLine("Constraint Error: " + ex.Message);
            }

            var Client = BookingManager.LoadClients();
            cmbClients.DataSource = Client;
            cmbClients.DisplayMember = "FullName";
            cmbClients.ValueMember = "ClientID";
        }


        private void Editbtn_Click(object sender, EventArgs e)
        {
            DateTime date = dtpBookedDate.Value.Date;
            DateTime time = dtpTime.Value;

            CalendarRecords.BookedDate = date.Add(time.TimeOfDay);
            try
            {
                CalendarRecords.BookingsID = Convert.ToInt32(txtBookingID.Text);
                CalendarRecords.ClientID = Convert.ToInt32(txtClientID.Text);

                if (!decimal.TryParse(lblTotalAmount.Text.Replace("₱", "").Trim(), out decimal totalAmount))
                {
                    MessageBox.Show("Total amount is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!manager.AreAllServicesAvailable(dgvRecords))
                {
                    MessageBox.Show("Booking failed: Insufficient inventory for the selected service.");
                    return;
                }
                bool success = manager.EditBoking(cmbStatus, cmbPaymentStatus,CalendarRecords.BookedDate, CalendarRecords.BookingsID, dgvRecords);
                if (success)
                {
                    MessageBox.Show("Booking updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    UpdateTotalAmount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Helpers
        private void Clear()
        {
            txtBookingID.Text = string.Empty;
            txtClientID.Text = string.Empty;
            dgvRecords.DataSource = null;
            lblTotalAmount.Text = "";
        }
        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgvRecords.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells["Hoursrender"].Value != null && row.Cells["Hoursrender"].Value != DBNull.Value &&
                    row.Cells["Rates"].Value != null && row.Cells["Rates"].Value != DBNull.Value)
                {
                    decimal newAmount = Convert.ToDecimal(row.Cells["Hoursrender"].Value) * Convert.ToDecimal(row.Cells["Rates"].Value);
                    totalAmount += newAmount;
                }
                lblTotalAmount.Text = $"{totalAmount:C}";
            }
        }
        private void LoadRecords()
        {
            try
            {
                manager.LoadBookingDate(selectedDate, txtBookingID, txtClientID, cmbStatus, cmbPaymentStatus, dgvRecords);
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void dgvRecords_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvRecords.Columns["Service"].Index)
                {
                    if (e.RowIndex < 0 || dgvRecords.Rows[e.RowIndex].IsNewRow)
                        return;

                    var cell = dgvRecords.Rows[e.RowIndex].Cells["Service"];
                    if (cell.Value == null || cell.Value == DBNull.Value)
                        return;

                    string serviceName = cell.Value.ToString();
                    if (!string.IsNullOrEmpty(serviceName))
                    {
                        manager.AddService(dgvRecords, e.RowIndex, UpdateTotalAmount);
                    }
                }
                else if (e.ColumnIndex == dgvRecords.Columns["Hoursrender"].Index)
                {
                    var rateCell = dgvRecords.Rows[e.RowIndex].Cells["Rates"];
                    if (rateCell.Value != null && rateCell.Value != DBNull.Value)
                    {
                        UpdateTotalAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
