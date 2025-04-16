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
    public partial class QuestionForm: Form
    {
        private Days day = new Days();

        private string dayValue;
        public QuestionForm(string day)
        {
            InitializeComponent();
            dayValue = day;

            //dtpTime.Value = DateTime.Today.AddHours(8);

            //if (selectedDate >= DateTime.Now)
            //{
            //    dtpBookedDate.Value = selectedDate;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
              DateTime selectedDate = new DateTime(Calendar.staticYear, Calendar.staticMonth, Convert.ToInt32(dayValue));
            foreach (Form form in Application.OpenForms)
            {
                if (form is Mainform mainform)
                {
                    BookingManagement booking = new BookingManagement(selectedDate);
                    mainform.OpenFormInPanel(booking);
                    break;

                }
            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = new DateTime(Calendar.staticYear, Calendar.staticMonth, Convert.ToInt32(dayValue));
            EditEvent editor = new EditEvent(Convert.ToInt32(dayValue), selectedDate);
            editor.Show();
            this.Hide();

        }
    }
}
