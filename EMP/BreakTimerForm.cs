using EMP.Models;
using System;
using System.Windows.Forms;

namespace EMP
{
    public partial class BreakTimerForm : Form
    {

        private int _timeLeft;
        private int _breakId;
        private frmdashboard _dashboard;
        public BreakTimerForm(frmdashboard dashboard,int breakId, int maxBreakTime)
        {
            InitializeComponent();
            _dashboard = dashboard;
            _timeLeft = maxBreakTime * 60;
            _breakId = breakId;
            lblTimer.Text = TimeSpan.FromSeconds(_timeLeft).ToString(@"mm\:ss");
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                TimeSpan timeSpan = TimeSpan.FromSeconds(_timeLeft);
                lblTimer.Text = timeSpan.ToString(@"mm\:ss");
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Break time is over!");
                this.Close();
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            _dashboard.PunchBreakOut(_breakId);
            this.Close();
        }
        private void BreakTimerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
