using System;
using System.Globalization;
using System.Windows.Forms;

namespace AlexisConstruction.Forms
{
    public partial class Calendar : Form
    {
        int year, month;
        public static int staticYear;
        public static int staticMonth;
        public Calendar()
        {
            InitializeComponent();
        }
        private void ScheduleCalendar_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            DateTime dateTime = DateTime.Now;
            year = dateTime.Year;
            month = dateTime.Month;
            DisplaySchedule();
        }
        private void DisplaySchedule()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

               
                if (month > 12)
                {
                    month = 1;
                    year++;
                }
                else if (month < 1)
                {
                    month = 12;
                    year--;
                }

                staticYear = year;
                staticMonth = month;

                string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
                lblMonthYear.Text = $"{monthName} {year}";

                // First day of the month
                DateTime startDate = new DateTime(year, month, 1);

                // Count of days in the month
                int daysInMonth = DateTime.DaysInMonth(year, month);

                // Convert startDateMonth to the first day of the week 
                int daysOfWeek = Convert.ToInt32(startDate.DayOfWeek.ToString("d"));

                //blank for the first day of the month
                for (int i = 0; i < daysOfWeek; i++)
                {
                    CalendarBlanks calendarBlank = new CalendarBlanks();
                    flowLayoutPanel1.Controls.Add(calendarBlank);
                }
                //frm for the days of the month
                for (int i = 1; i <= daysInMonth; i++)
                {
                    Days days = new Days();
                    days.DisplayDays(i);
                    flowLayoutPanel1.Controls.Add(days);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Previewbtn_Click(object sender, EventArgs e)
        {
            month--;
            DisplaySchedule();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is Mainform mainForm)
                {
                    BookingManagement bookingForm = new BookingManagement(DateTime.Now);
                    mainForm.OpenFormInPanel(bookingForm);
                    break;
                }
            }
        }

        private void Nextbtn_Click(object sender, EventArgs e)
        {
            month++;
            DisplaySchedule();
        }
    }
}
