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

        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            Dashboard bookInfoForm = new Dashboard();
            form.OpenForm(bookInfoForm, paneldash);
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

                switch (button.Name)
                {
                    case "Sched":
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
                    case "Records":
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
