namespace EMP
{
    partial class frmdashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmdashboard));
            this.btnbegin = new System.Windows.Forms.Button();
            this.pnllogin = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnlogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblshortname = new System.Windows.Forms.Label();
            this.btnbreak = new System.Windows.Forms.Button();
            this.lblname = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblemail = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblcurrenttime = new System.Windows.Forms.Label();
            this.lbltimer = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnlogout = new System.Windows.Forms.Button();
            this.lbllastsync = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.pnllogin.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnbegin
            // 
            this.btnbegin.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnbegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbegin.ForeColor = System.Drawing.SystemColors.Control;
            this.btnbegin.Location = new System.Drawing.Point(42, 152);
            this.btnbegin.Name = "btnbegin";
            this.btnbegin.Size = new System.Drawing.Size(241, 64);
            this.btnbegin.TabIndex = 0;
            this.btnbegin.Text = "Punch in";
            this.btnbegin.UseVisualStyleBackColor = false;
            this.btnbegin.Click += new System.EventHandler(this.btnbegin_Click);
            // 
            // pnllogin
            // 
            this.pnllogin.BackColor = System.Drawing.Color.SkyBlue;
            this.pnllogin.Controls.Add(this.panel1);
            this.pnllogin.Controls.Add(this.btnlogin);
            this.pnllogin.Controls.Add(this.label2);
            this.pnllogin.Controls.Add(this.label1);
            this.pnllogin.Controls.Add(this.txtpassword);
            this.pnllogin.Controls.Add(this.txtusername);
            this.pnllogin.Location = new System.Drawing.Point(358, 12);
            this.pnllogin.Name = "pnllogin";
            this.pnllogin.Size = new System.Drawing.Size(334, 408);
            this.pnllogin.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::EMP.Properties.Resources.hublog;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 132);
            this.panel1.TabIndex = 5;
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.ForeColor = System.Drawing.Color.White;
            this.btnlogin.Location = new System.Drawing.Point(139, 256);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(149, 51);
            this.btnlogin.TabIndex = 4;
            this.btnlogin.Text = "LOGIN";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(50, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Name";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(139, 209);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(149, 20);
            this.txtpassword.TabIndex = 1;
            this.txtpassword.Text = "123456";
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(139, 169);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(149, 20);
            this.txtusername.TabIndex = 0;
            this.txtusername.Text = "vetri@Hublog.com";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.lblshortname);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 107);
            this.panel2.TabIndex = 6;
            // 
            // lblshortname
            // 
            this.lblshortname.AutoSize = true;
            this.lblshortname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblshortname.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshortname.ForeColor = System.Drawing.Color.White;
            this.lblshortname.Location = new System.Drawing.Point(6, 26);
            this.lblshortname.Name = "lblshortname";
            this.lblshortname.Size = new System.Drawing.Size(87, 54);
            this.lblshortname.TabIndex = 2;
            this.lblshortname.Text = "VV";
            this.lblshortname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnbreak
            // 
            this.btnbreak.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnbreak.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbreak.ForeColor = System.Drawing.Color.White;
            this.btnbreak.Location = new System.Drawing.Point(199, 15);
            this.btnbreak.Name = "btnbreak";
            this.btnbreak.Size = new System.Drawing.Size(84, 33);
            this.btnbreak.TabIndex = 2;
            this.btnbreak.Text = "Break";
            this.btnbreak.UseVisualStyleBackColor = false;
            this.btnbreak.Visible = false;
            this.btnbreak.Click += new System.EventHandler(this.btnbreak_Click);
            // 
            // lblname
            // 
            this.lblname.AutoSize = true;
            this.lblname.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblname.ForeColor = System.Drawing.Color.White;
            this.lblname.Location = new System.Drawing.Point(5, 17);
            this.lblname.Name = "lblname";
            this.lblname.Size = new System.Drawing.Size(111, 26);
            this.lblname.TabIndex = 1;
            this.lblname.Text = "Welcome";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 437);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(685, 112);
            this.dataGridView1.TabIndex = 7;
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // mynotifyicon
            // 
            this.mynotifyicon.BalloonTipText = "HUBLOO";
            this.mynotifyicon.BalloonTipTitle = "HUBLOO";
            this.mynotifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("mynotifyicon.Icon")));
            this.mynotifyicon.Text = "HUBLOO";
            this.mynotifyicon.Visible = true;
            this.mynotifyicon.DoubleClick += new System.EventHandler(this.mynotifyicon_DoubleClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Azure;
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.panel4);
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(334, 408);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.BlueViolet;
            this.panel3.Controls.Add(this.lblemail);
            this.panel3.Controls.Add(this.lblname);
            this.panel3.Location = new System.Drawing.Point(108, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(223, 107);
            this.panel3.TabIndex = 7;
            // 
            // lblemail
            // 
            this.lblemail.AutoSize = true;
            this.lblemail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblemail.ForeColor = System.Drawing.Color.White;
            this.lblemail.Location = new System.Drawing.Point(5, 62);
            this.lblemail.Name = "lblemail";
            this.lblemail.Size = new System.Drawing.Size(82, 20);
            this.lblemail.TabIndex = 2;
            this.lblemail.Text = "Welcome";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.lblcurrenttime);
            this.panel4.Controls.Add(this.lbltimer);
            this.panel4.Controls.Add(this.btnbegin);
            this.panel4.Controls.Add(this.btnbreak);
            this.panel4.Location = new System.Drawing.Point(3, 116);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(328, 228);
            this.panel4.TabIndex = 8;
            // 
            // lblcurrenttime
            // 
            this.lblcurrenttime.AutoSize = true;
            this.lblcurrenttime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcurrenttime.Location = new System.Drawing.Point(95, 127);
            this.lblcurrenttime.Name = "lblcurrenttime";
            this.lblcurrenttime.Size = new System.Drawing.Size(148, 15);
            this.lblcurrenttime.TabIndex = 4;
            this.lblcurrenttime.Text = "Tue 16 Apr 2024 12.49.58";
            // 
            // lbltimer
            // 
            this.lbltimer.AutoSize = true;
            this.lbltimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltimer.Location = new System.Drawing.Point(66, 65);
            this.lbltimer.Name = "lbltimer";
            this.lbltimer.Size = new System.Drawing.Size(213, 54);
            this.lbltimer.TabIndex = 3;
            this.lbltimer.Text = "00:00:00";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SkyBlue;
            this.panel5.Controls.Add(this.button2);
            this.panel5.Controls.Add(this.btnlogout);
            this.panel5.Controls.Add(this.lbllastsync);
            this.panel5.Location = new System.Drawing.Point(3, 350);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(329, 56);
            this.panel5.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImage = global::EMP.Properties.Resources.recycle;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(10, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(31, 25);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnlogout
            // 
            this.btnlogout.BackColor = System.Drawing.Color.White;
            this.btnlogout.BackgroundImage = global::EMP.Properties.Resources.logout2;
            this.btnlogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnlogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogout.ForeColor = System.Drawing.Color.White;
            this.btnlogout.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnlogout.Location = new System.Drawing.Point(284, 9);
            this.btnlogout.Name = "btnlogout";
            this.btnlogout.Size = new System.Drawing.Size(39, 37);
            this.btnlogout.TabIndex = 6;
            this.btnlogout.UseVisualStyleBackColor = false;
            this.btnlogout.Click += new System.EventHandler(this.btnlogout_Click);
            // 
            // lbllastsync
            // 
            this.lbllastsync.AutoSize = true;
            this.lbllastsync.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllastsync.Location = new System.Drawing.Point(46, 24);
            this.lbllastsync.Name = "lbllastsync";
            this.lbllastsync.Size = new System.Drawing.Size(151, 15);
            this.lbllastsync.TabIndex = 5;
            this.lbllastsync.Text = "Last Sync 13 mins Ago";
            // 
            // timer3
            // 
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // frmdashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(699, 428);
            this.Controls.Add(this.pnllogin);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmdashboard";
            this.ShowIcon = false;
            this.Text = "HUBLOO";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmdashboard_FormClosing);
            this.Load += new System.EventHandler(this.frmdashboard_Load);
            this.Resize += new System.EventHandler(this.frmdashboard_Resize);
            this.pnllogin.ResumeLayout(false);
            this.pnllogin.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnbegin;
        private System.Windows.Forms.Panel pnllogin;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblname;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnbreak;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblshortname;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblemail;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblcurrenttime;
        private System.Windows.Forms.Label lbltimer;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnlogout;
        private System.Windows.Forms.Label lbllastsync;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
    }
}

