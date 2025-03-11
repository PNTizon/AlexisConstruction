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
        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Click(object sender, EventArgs e)
        {
            FormManager form = new FormManager();

            if (sender is Button button)
            {
                Dashboardbtn.Name = "Dash";
                BookingManagerbtn.Name = "Booking";
                ClientManagerbtn.Name = "Client";
                ServiceManagerbtn.Name = "Services";
                InventoryManagerbtn.Name = "Inventory";
                PaymentManagerbtn.Name = "Payment";
                BillingStatement.Name = "Billings";

                switch (button.Name)
                {
                    case "Dash":
                        Dashboard dVDs_VCDs = new Dashboard();
                        form.OpenForm(dVDs_VCDs, paneldash);
                        break;
                    case "Booking":
                        BookingManagement customer = new BookingManagement();
                        form.OpenForm(customer, paneldash);
                        break;
                    case "Client":
                        ClientManagement trans = new ClientManagement();
                        form.OpenForm(trans, paneldash);
                        break;
                    case "Services":
                        ServiceManagement reports = new ServiceManagement();
                        form.OpenForm(reports, paneldash);
                        break;
                    case "Inventory":
                        InventoryManagement inventory = new InventoryManagement();
                        form.OpenForm(inventory, paneldash);
                        break;
                    case "Payment":
                        PaymentManagement payment = new PaymentManagement();
                        form.OpenForm(payment, paneldash);
                        break;
                    case "Billings":
                        BillingStatementForm billing = new BillingStatementForm();
                        form.OpenForm(billing, paneldash);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
