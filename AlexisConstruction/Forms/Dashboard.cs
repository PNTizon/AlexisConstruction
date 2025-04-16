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
    public partial class Dashboard: Form
    {
        private DashboardManager manager = new DashboardManager();
        public Dashboard()
        {
            InitializeComponent();
            LoadTodaySchedule();
            LoadClients();
            LoadPendings();
            CancelledRecords();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Mainform mainForm)
                {
                    Button highlightbtn = mainForm.GetButton("Sched");

                    if (highlightbtn != null)
                    {
                        TodaySchedule schedule = new TodaySchedule();
                        mainForm.OpenFormInPanel(schedule);
                        mainForm.ResetButtonColor();
                        mainForm.HighlightButton(highlightbtn);
                    }
                    break;
                }
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if(form is Mainform mainform)
                {
                    Button highlightbtn = mainform.GetButton("Payment");
                    if (highlightbtn != null)
                    {
                        PaymentManagement booking = new PaymentManagement();
                        {
                            PaymentManagement.ShowUnpaidRecords = true;
                        }
                        mainform.OpenFormInPanel(booking);
                        mainform.ResetButtonColor();
                        mainform.HighlightButton(highlightbtn);
                    }
                    break;
                }
            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if (form is Mainform mainform)
                {
                    Button highlightbtn = mainform.GetButton("Records");
                    if (highlightbtn != null)
                    {
                        BillingStatementForm booking = new BillingStatementForm();
                        {
                            BillingStatementForm.ShowCancelledRecords = true;
                        }
                        mainform.OpenFormInPanel(booking);
                        mainform.ResetButtonColor();
                        mainform.HighlightButton(highlightbtn);
                    }
                    break;
                }
            }
        }

       
        private void panel6_Click_1(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Mainform mainForm)
                {
                    Button highlightbtn = mainForm.GetButton("Client");

                    if (highlightbtn != null)
                    {
                        ClientManagement schedule = new ClientManagement();
                        mainForm.OpenFormInPanel(schedule);
                        mainForm.ResetButtonColor();
                        mainForm.HighlightButton(highlightbtn);
                    }
                    break;
                }
            }
        }

        #region Methods
        public void LoadTodaySchedule()
        {
            manager.DisplayTodaySchedule(lblSchedule);
        }
        public void LoadClients()
        {
            manager.DisplayClients(lblClients);
        }
        public void LoadPendings()
        {
            manager.LoadPendingPayments(lblPendingPayments);
        }
        public void CancelledRecords()
        {
            manager.LoadCancelledRecords(lblCancelled);
        }
        #endregion
    }
}
