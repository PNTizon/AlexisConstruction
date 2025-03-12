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
    public partial class PaymentManagement : Form
    {
        private PaymentManager paymentProcessor = new PaymentManager();

        private Display Display = new Display();
        
        public PaymentManagement()
        {
            InitializeComponent();
        }
        private void LoadBillingInfo()
        {
            Display.GetAllPayments(dgvBilling);
        }
        private void PaymentManagement_Load(object sender, EventArgs e)
        {
            LoadBillingInfo();
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            if(dgvBilling.SelectedRows.Count > 0)
            {
                int billingID = Convert.ToInt32(dgvBilling.SelectedRows[0].Cells["BookingID"].Value);

                Bookings billingInfo = paymentProcessor.GetBillingInfo(billingID);
                if(billingInfo != null)
                {
                    if(paymentProcessor.ProcessPayment(billingInfo))
                    {
                        MessageBox.Show("Payment successful!");
                        LoadBillingInfo();
                    }
                    else { MessageBox.Show("Payment failed."); }
                }
                else { MessageBox.Show("Billing record not found."); }
            }
            else { MessageBox.Show("Please select a billing record."); }
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
    }
}
