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
    public partial class Dashboard : Form
    {
        private Display display = new Display();
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            display.LoadWeeklySchedule(dgvSchedule);

            Timer timer = new Timer();
            timer.Interval = 60000; //1 min ni
            timer.Tick += (s, args) => display.CheckAndUpdateCompletedServices(dgvSchedule);
            timer.Start();
        }
    }
}
