﻿using EMP.Models;
using log4net.Repository.Hierarchy;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMP
{
    public partial class frmbreak : Form
    {
        public frmbreak()
        {
            InitializeComponent();
        }
        List<BreakMaster> objlist = new List<BreakMaster>();
        List<RadioButton2> blist = new List<RadioButton2>();
        public class RadioButton2 : RadioButton
        {
            public string GroupName { get; set; }
        }
        private void RadioButton2_Clicked(object sender, EventArgs e)
        {
            RadioButton2 rb = (sender as RadioButton2);
            for (int i = 0; i < blist.Count; i++)
            {
                if (rb.Name == blist[i].Name)
                {
                    blist[i].Checked = true;
                }
                else
                {
                    blist[i].Checked = false;
                }
            }
        }

        private DateTime GetISTTime()
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utcTime = DateTime.UtcNow;
            DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, istTimeZone);
            return istTime;
        }

        #region old punchout
        public void punchbreakout()
        {
            GetModels LM = new GetModels();
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            LM.UserId = Program.Loginlist.Id;
            DateTime istTime = GetISTTime();
            LM.CDate =  istTime;
            string master = JsonConvert.SerializeObject(LM);
            string URL = Program.OnlineURL + "api/Users/GetAvailableBreak";
            string DATA = master;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
            client1B.BaseAddress = new System.Uri(URL);
            client1B.Timeout = TimeSpan.FromMinutes(30);
            //client1B.DefaultRequestHeaders.Add("Idlist", UpdateIdList);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            if (messge1B.IsSuccessStatusCode)
            {
                var objresult = JsonConvert.DeserializeObject<List<BreakMaster>>(responseString1B);
                objlist = objresult;
            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);

            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            punchbreakout();

            for (int i = 0; i < objlist.Count; i++)
            {
                Panel p = new Panel
                {
                    Margin = new Padding(left: 30, top: 10, bottom: 0, right: 0),
                    Size = new Size(310, 30)
                };

                RadioButton2 rb1 = new RadioButton2
                {
                    Text = objlist[i].Name,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    Name = objlist[i].Id.ToString(),
                    Tag = new BreakInfo { Id = objlist[i].Id, Max_Break_Time = objlist[i].Max_Break_Time }
                    //Tag = objlist[i].Id,
                };

                if (i == 0)
                {
                    rb1.Checked = true;
                }

                Console.WriteLine(rb1);

                rb1.Click += RadioButton2_Clicked;
                Controls.Add(rb1);
                blist.Add(rb1);
                p.Controls.Add(rb1);
                panel.Controls.Add(p);
            }

            panel.AutoScroll = false;
            panel.HorizontalScroll.Enabled = false;
            panel.HorizontalScroll.Visible = false;
            panel.HorizontalScroll.Maximum = 0;
            panel.AutoScroll = true;
        }

        private void btnbreak_Click(object sender, EventArgs e)
        {
            var selectedBreak = blist.FirstOrDefault(c => c.Checked);

            if (selectedBreak != null && selectedBreak.Tag is BreakInfo breakInfo)
            {
                var breakDetails = new BreakInfo
                {
                    Id = breakInfo.Id,
                    Max_Break_Time = breakInfo.Max_Break_Time,
                };

                frmdashboard.BreakInfo = breakDetails;
                this.Close();
            }
        }
    }
}
