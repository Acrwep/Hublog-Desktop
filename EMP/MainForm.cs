using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Hublog
{
    public partial class MainForm : Form
    {
        private DateTime targetTime;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            LoadTimerState();
            SetupTimer();
        }

        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            UpdateTimerUI();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= targetTime)
            {
                timer.Stop();
                TimerFinished();
            }
            else
            {
                UpdateTimerUI();
            }
        }

        private void UpdateTimerUI()
        {
            TimeSpan remainingTime = targetTime - DateTime.Now;
            lblTimer.Text = remainingTime.ToString(@"hh\:mm\:ss");
        }

        private void SaveTimerState()
        {
            TimeSpan remainingTime = targetTime - DateTime.Now;
            File.WriteAllText("timerState.txt", remainingTime.ToString());
        }

        private void LoadTimerState()
        {
            if (File.Exists("timerState.txt"))
            {
                string remainingTimeStr = File.ReadAllText("timerState.txt");
                if (TimeSpan.TryParse(remainingTimeStr, out TimeSpan remainingTime))
                {
                    targetTime = DateTime.Now.Add(remainingTime);
                    UpdateTimerUI();
                    timer.Start();
                }
            }
        }

        private void CleanUpTimerState()
        {
            if (File.Exists("timerState.txt"))
            {
                File.Delete("timerState.txt");
            }
        }

        private void TimerFinished()
        {
            CleanUpTimerState();
            MessageBox.Show("Time's up!");
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTimerState();
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            SaveTimerState();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveTimerState();
            base.OnFormClosing(e);
        }
    }
}
