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
    public partial class TodaySchedule : Form
    {
        private Display display = new Display();
        public TodaySchedule()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            display.LoadWeeklySchedule(dgvSchedule);

            timer1.Tick += (s, args) => display.CheckAndUpdateCompletedServices(dgvSchedule);

            timer1.Start();
        }
    }
}
