﻿using AlexisConstruction.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class BillingStatementForm : Form
    {
        private DataTable originalTable;
        public static bool ShowCancelledRecords { get; set; } = false;
        public BillingStatementForm()
        {
            InitializeComponent();
        }

        public void LoadBillingReport(string clientName)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("LoadBillingByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchName", "%" + clientName + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvReports.DataSource = dt;

                if (dgvReports.Columns["ServicesAvailed"] != null)
                    dgvReports.Columns["ServicesAvailed"].Visible = false;
            }
        }
        private void LoadBillingByDate(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("LoadBillingByDate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                originalTable = dt.Copy();
                dgvReports.DataSource = dt;

                if (dgvReports.Columns["ServicesAvailed"] != null)
                    dgvReports.Columns["ServicesAvailed"].Visible = false;
            }
        }
        private void LoadPaymenTStatus(string paymentStatus)
        {
            using (SqlConnection conn = new SqlConnection(Connection.Database))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("LoadBillingData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Status", paymentStatus);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                originalTable = dt.Copy();
                dgvReports.DataSource = dt;

                if (dgvReports.Columns["ServicesAvailed"] != null)
                    dgvReports.Columns["ServicesAvailed"].Visible = false;
            }
        }
        private void BillingStatementForm_Load(object sender, EventArgs e)
        {
            try
            {
                // TODO: This line of code loads data into the 'dataSet2.BILLINGSTATEMENT1' table. You can move, or remove it, as needed.
                this.bILLINGSTATEMENT1TableAdapter1.Fill(this.dataSet2.BILLINGSTATEMENT1);

                if (ShowCancelledRecords)
                {
                    comboBox1.SelectedItem = "Cancelled";

                    DataView dataView = new DataView(this.dataSet2.BILLINGSTATEMENT1);
                    dataView.RowFilter = "[Status] = 'Cancelled'";

                    dgvReports.DataSource = dataView;
                }
            }
            catch
            {
                MessageBox.Show("An error occurred while loading the form.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItems = comboBox1.SelectedItem.ToString();

            if (selectedItems == "All")
            {
                dgvReports.DataSource = this.dataSet2.BILLINGSTATEMENT1;
            }
            else
            {
                LoadPaymenTStatus(selectedItems);
            }
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    GenerateReceipt();
        //}
        private void GenerateReceipt()
        {
            if (dgvReports.SelectedRows.Count > 0 || dgvReports.CurrentRow != null)
            {
                DataGridViewRow row = dgvReports.SelectedRows.Count > 0
                    ? dgvReports.SelectedRows[0]
                    : dgvReports.CurrentRow;

                try
                {
                    if (dgvReports.SelectedRows.Count > 0)
                    {
                        BillingRecords bookings = new BillingRecords();

                        bookings.CustomerName = row.Cells["ClientName"].Value.ToString();
                        bookings.BookingsID = Convert.ToInt32(row.Cells["BookingID"].Value);
                        bookings.BillingDate = Convert.ToDateTime(row.Cells["BookedDate"].Value);
                        bookings.BookedDate = Convert.ToDateTime(row.Cells["BookedDate"].Value);
                        bookings.MOP = "Cash";

                        bookings.BookingReceipt = BookingManager.GetServiceDetails(bookings.BookingsID);

                        using (printReceipt receipt = new printReceipt(bookings, bookings.BookingReceipt))
                        {
                            receipt.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a record to generate a receipt.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error occured while trying to print the receipt.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a record to generate a receipt.");
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GenerateReceipt();
            }
        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            LoadBillingReport(txtSearchBox.Text);
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadBillingByDate(dtpFrom.Value, dtpTo.Value);
        }
    }
}
