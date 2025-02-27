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
        private int billingID;

        public PaymentManagement(int billingID)
        {
            InitializeComponent();
            this.billingID = billingID;
            LoadBillingInfo();
        }
        public PaymentManagement()
        {
            InitializeComponent();
        }
        private void dgvBilling_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvBilling.Columns["PaymentStatus"].Index && e.RowIndex >= 0)
            {
                int billingID = (int)dgvBilling.Rows[e.RowIndex].Cells["BillingID"].Value;
            }
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
            Billing billing = paymentProcessor.GetBillingInfo(billingID);
            if (billing != null)
            {

                if (decimal.TryParse(txtAmountPaid.Text, out decimal amountPaid) && amountPaid > 0)
                {
                    Payment payment = new Payment
                    {
                        BillingID = billingID,
                        PaymentDate = DateTime.Now,
                        AmountPaid = amountPaid
                    };

                    if (paymentProcessor.ProcessPayment(payment))
                    {
                        MessageBox.Show("Payment processed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBillingInfo();
                    }
                    else
                    {
                        MessageBox.Show("Failed to process payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid payment amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Billing information not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
