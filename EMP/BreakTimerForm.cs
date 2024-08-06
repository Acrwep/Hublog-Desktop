using EMP.Models;
using System;
using System.Windows.Forms;

namespace EMP
{
    public partial class BreakTimerForm : Form
    {

        private int _timeLeft;
        private int _breakId;
        private string _breakname;
        private frmdashboard _dashboard;
        private bool _timerStopped;
        public BreakTimerForm(frmdashboard dashboard,int breakId, int maxBreakTime, string breakname)
        {
            InitializeComponent();
            _dashboard = dashboard;
            _timeLeft = maxBreakTime * 60;
            _breakId = breakId;
            lblTimer.Text = TimeSpan.FromSeconds(_timeLeft).ToString(@"mm\:ss");
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
            _breakname = breakname;
            _timerStopped = false;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                lblBreakStatus.Text = "You are in.."; // + _breakname;
                lblBreakname.Text = _breakname;
                lblBreakname.ForeColor = System.Drawing.Color.Green;
                TimeSpan timeSpan = TimeSpan.FromSeconds(_timeLeft);
                lblTimer.Text = timeSpan.ToString(@"mm\:ss");
            }
            else
            {
                timer1.Stop();
                _timerStopped = true;
                lblBreakStatus.Text = "Break time is over!";
                lblBreakStatus.ForeColor = System.Drawing.Color.Black;
                lblBreakStatus.Location = new System.Drawing.Point(60, 10);
                btnResume.BackColor = System.Drawing.Color.Red;
                btnResume.Text = "Resume Working (Time Up)";

                lblBreakname.Visible = false;
            }
        }

        //private void btnResume_Click(object sender, EventArgs e)
        //{
        //    _dashboard.PunchBreakOut(_breakId);
        //    this.Close();
        //}

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (_timerStopped) 
            {
                _dashboard.PunchBreakOut(_breakId);
                this.Close();
            }
            else
            {
                Console.WriteLine("timer is still running");
            }
        }
    }
}
