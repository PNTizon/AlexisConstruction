using AlexisConstruction.Classes;
using System;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class Days : UserControl
    {
        public static string days;
        private DayManager day = new DayManager();

        public Days()
        {
            InitializeComponent();
        }

        private void Days_Load(object sender, EventArgs e)
        {

        }

        private void Days_Click(object sender, EventArgs e)
        {
            days = lblDays.Text;
            timer1.Start();

            if (int.TryParse(lblDays.Text, out int day))
            {
                if (day < 1 || day > DateTime.DaysInMonth(Calendar.staticYear, Calendar.staticMonth))
                {
                    return;
                }
                DateTime selectedDate = new DateTime(Calendar.staticYear, Calendar.staticMonth, Convert.ToInt32(lblDays.Text));
                QuestionForm question = new QuestionForm(lblDays.Text);
                question.Show();

                //EditEvent editor = new EditEvent(Convert.ToInt32(lblDays.Text), selectedDate);
                //editor.Show();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            day.DisplayEvent(lblEvent, toolTip1, lblDays, this);
        }
        public void DisplayDays(int numDays)
        {
            lblDays.Text = numDays.ToString();

            DateTime currentdate = new DateTime(Calendar.staticYear, Calendar.staticMonth, numDays);

            day.DisplayEvent(lblEvent, toolTip1, lblDays, this);
            lblDays.ForeColor = day.GetDayColor(currentdate);
        }

        private void lblEvent_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = new DateTime(Calendar.staticYear, Calendar.staticMonth, Convert.ToInt32(lblDays.Text));

            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form is Mainform mainform)
            //    {
            //        BookingManagement booking = new BookingManagement(selectedDate);
            //        mainform.OpenFormInPanel(booking);
            //        break;
            //    }
            //}
        }
        public DateTime GetDate()
        {
            if (int.TryParse(lblDays.Text, out int day))
            {
                return new DateTime(Calendar.staticYear, Calendar.staticMonth, day);
            }
            else
            {
                throw new InvalidOperationException("Invalid date");
            }

        }
    }
}
