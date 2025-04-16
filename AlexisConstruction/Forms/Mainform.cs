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
    public partial class Mainform : Form
    {
        private FormManager form = new FormManager();
        public Panel pnldash;
        private Button activebtn = null;

        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            Dashboard bookInfoForm = new Dashboard();
            form.OpenForm(bookInfoForm, paneldash);
            HighlightButton(Dashboardbtn);
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                Schedulebtn.Name = "Sched";
                BookingManagerbtn.Name = "Booking";
                ClientManagerbtn.Name = "Client";
                ServiceManagerbtn.Name = "Services";
                InventoryManagerbtn.Name = "Inventory";
                BillingStatement.Name = "Records";
                Paymentbtn.Name = "Payment";
                Calendarbtn.Name = "Calendar";
                Dashboardbtn.Name = "Dashboard";


                ResetButtonColor();
                HighlightButton(button);

                switch (button.Name)
                {
                    case "Dashboard":
                        form.OpenForm(new Dashboard(), paneldash);
                        break;
                    case "Sched":
                        form.OpenForm(new TodaySchedule(), paneldash);
                        break;
                    case "Booking":
                        form.OpenForm(new BookingManagement(), paneldash);
                        break;
                    case "Client":
                        form.OpenForm(new ClientManagement(), paneldash);
                        break;
                    case "Services":
                        form.OpenForm(new ServiceManagement(), paneldash);
                        break;
                    case "Inventory":
                        form.OpenForm(new InventoryManagement(), paneldash);
                        break;
                    case "Records":
                        form.OpenForm(new BillingStatementForm(), paneldash);
                        break;
                    case "Payment":
                        form.OpenForm( new PaymentManagement(), paneldash);
                        break;
                    case "Calendar":
                        form.OpenForm(new Calendar(), paneldash);
                        break;
                    default:
                        break;
                }
            }
        }
      
        public void HighlightButton(Button button)
        {
            activebtn = button;
            activebtn.BackColor = Color.FromArgb(141, 173, 104);
            activebtn.ForeColor = Color.White;
            activebtn.Font = new Font(activebtn.Font, FontStyle.Bold);
        }
        public void ResetButtonColor()
        {
            if(activebtn != null)
            {
                activebtn.BackColor = Color.FromArgb(67, 100, 54);
                activebtn.ForeColor = Color.White;
                activebtn.Font = new Font(activebtn.Font, FontStyle.Regular);
            }
        }

        public void OpenFormInPanel(Form formToOpen)
        {
            paneldash.Controls.Clear();

            // Set form properties
            formToOpen.TopLevel = false;
            formToOpen.FormBorderStyle = FormBorderStyle.None;
            formToOpen.Dock = DockStyle.Fill;

            paneldash.Controls.Add(formToOpen);
            formToOpen.Show();
        }
        public Button GetButton(string buttonName)
        {
            switch(buttonName)
            {
                case "Sched":
                    return Schedulebtn;
                case "Booking":
                    return BookingManagerbtn;
                case "Client":
                    return ClientManagerbtn;
                case "Services":
                    return ServiceManagerbtn;
                case "Inventory":
                    return InventoryManagerbtn;
                case "Records":
                    return BillingStatement;
                case "Payment":
                    return Paymentbtn;
                case "Calendar":
                    return Calendarbtn;
                case "Dashboard":
                    return Dashboardbtn;
                default:
                    return null;
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
