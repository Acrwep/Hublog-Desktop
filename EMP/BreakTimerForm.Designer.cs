
namespace EMP
{
    partial class BreakTimerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnResume;

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
            this.SuspendLayout();
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblTimer.Location = new System.Drawing.Point(50, 50);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(200, 50);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnResume
            // 
            // btnResume
            // btnResume
            this.btnResume = new System.Windows.Forms.Button();
            this.btnResume.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResume.Location = new System.Drawing.Point(85, 120);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(130, 40);
            this.btnResume.TabIndex = 1;
            this.btnResume.Text = "Resume";
            this.btnResume.UseVisualStyleBackColor = false;
            this.btnResume.BackColor = System.Drawing.Color.Black; // Changed to black color
            this.btnResume.ForeColor = System.Drawing.Color.White;
            this.btnResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);


            // BreakTimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.lblTimer);
            this.Name = "BreakTimerForm";
            this.Text = "Break Timer";
            this.ResumeLayout(false);
        }
    }
}