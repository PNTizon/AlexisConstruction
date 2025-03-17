using AlexisConstruction.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class PaymentManagement : Form
    {
        private PaymentManager paymentProcessor = new PaymentManager();
        private Display Display = new Display();

        public PaymentManagement()
        {
            InitializeComponent();
        }
       
        private void PaymentManagement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet2.SHOWPAYMENTS' table. You can move, or remove it, as needed.
            this.sHOWPAYMENTSTableAdapter.Fill(this.dataSet2.SHOWPAYMENTS);
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            if (dgvBilling.SelectedRows.Count > 0)
            {
                int billingID = Convert.ToInt32(dgvBilling.SelectedRows[0].Cells["BookingID"].Value);

                Bookings billingInfo = paymentProcessor.GetBillingInfo(billingID);
                if (billingInfo != null)
                {
                    if (paymentProcessor.ProcessPayment(billingInfo))
                    {
                        MessageBox.Show("Payment successful!");
                        PrintBtn();
                        this.sHOWPAYMENTSTableAdapter.Fill(this.dataSet2.SHOWPAYMENTS);
                    }
                    else { MessageBox.Show("Payment failed."); }
                }
                else { MessageBox.Show("Billing record not found."); }
            }
            else { MessageBox.Show("Please select a billing record."); }
        }

        public void PrintBtn()
        {
            if (dgvBilling.SelectedRows.Count > 0)
            {
                Bookings booking = new Bookings();

                DataGridViewRow row = dgvBilling.SelectedRows[0];

                booking.CustomerName = row.Cells["ClientName"].Value.ToString();
                booking.BookingID = Convert.ToInt32(row.Cells["BookingID"].Value);
                booking.BookedDate = Convert.ToDateTime(row.Cells["BookedDate"].Value);
                booking.BillingDate =DateTime.Now;
                booking.TotalAmount = Convert.ToDecimal(row.Cells["TotalAmount"].Value);
                booking.PaymentMethod = "Cash";

                using (printReceipt receipt = new printReceipt(booking, booking.PaymentReceipt))
                {
                    receipt.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select a record to generate a receipt.");
            }
        }
        private void dgvBilling_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                int billingID = (int)dgvBilling.Rows[e.RowIndex].Cells["BookingID"].Value;
            }
        }
        private void searchbtn_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            paymentProcessor.SeacrhRecords(search, dgvBilling);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            paymentProcessor.SeacrhRecords(search, dgvBilling);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItems = comboBox1.SelectedItem.ToString();
            ((BindingSource)dgvBilling.DataSource).Filter = $"[PaymentStatus] = '{selectedItems}'";
        }
    }
}
