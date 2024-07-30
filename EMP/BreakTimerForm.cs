using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMP
{
    public partial class BreakTimerForm : Form
    {

        private int _timeLeft;
        public BreakTimerForm(int maxBreakTime)
        {
            InitializeComponent();
            _timeLeft = maxBreakTime * 60; // Convert minutes to seconds
            lblTimer.Text = TimeSpan.FromSeconds(_timeLeft).ToString(@"mm\:ss");
            timer1.Interval = 1000; // 1 second intervals
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
            this.Close();
        }
        private void BreakTimerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
