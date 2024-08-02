
using System.Windows.Forms;

namespace EMP
{
    partial class BreakTimerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Label lblBreakStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnResume = new System.Windows.Forms.Button();
            this.lblBreakStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblBreakStatus.AutoSize = true;
            this.lblBreakStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.lblBreakStatus.ForeColor = System.Drawing.Color.White;
            this.lblBreakStatus.Location = new System.Drawing.Point(27, 10);
            this.lblBreakStatus.Name = "lblBreakStatus";
            this.lblBreakStatus.Size = new System.Drawing.Size(165, 24);
            this.lblBreakStatus.TabIndex = 3;
            this.lblBreakStatus.Text = "You are in a break";

            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblTimer.Location = new System.Drawing.Point(50, 50);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(200, 50);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.btnResume = new System.Windows.Forms.Button();
            this.btnResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResume.Name = "btnResume";
            this.btnResume.TabIndex = 1;
            this.btnResume.Text = "Resume Working";
            this.btnResume.UseVisualStyleBackColor = false;
            this.btnResume.BackColor = System.Drawing.Color.Black;
            this.btnResume.ForeColor = System.Drawing.Color.White;
            this.btnResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);

            var textSize = TextRenderer.MeasureText(this.btnResume.Text, this.btnResume.Font);
            this.btnResume.Size = new System.Drawing.Size(textSize.Width + 25, textSize.Height + 10);
            this.btnResume.Location = new System.Drawing.Point(70, 120);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.lblBreakStatus);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.lblTimer);
            this.Name = "BreakTimerForm";
            this.Text = "Break Timer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}