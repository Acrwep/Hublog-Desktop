using EMP.Models;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
    public partial class frmdashboard : Form
    {
        public frmdashboard()
        {
            InitializeComponent();
            this.MinimizeBox = true;
        }

        public int currenttype = 0;
        public static BreakInfo BreakInfo;
        public string diff = "";
        Stopwatch SW = new Stopwatch();
        Stopwatch SW1 = new Stopwatch();
        DateTime Lastsync = new DateTime();
        private bool isLoggedIn = false;

        public List<string> addlist = new List<string>();

        private void frmdashboard_Load(object sender, EventArgs e)
        {
            pnllogin.Location = new Point() { X = 12, Y = 12 };
            this.Size = new Size() { Width = 379, Height = 580 };
            this.mynotifyicon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.mynotifyicon.ContextMenuStrip.Items.Add("QUIT", null, closeapp);
            timer3.Start();
            startup();

            this.mynotifyicon.Icon = Hublog.Properties.Resources.hublog1;
        }
        private void frmdashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                if (FormWindowState.Normal == this.WindowState)
                {
                    mynotifyicon.Visible = true;
                    this.Hide();
                }

                else if (FormWindowState.Minimized == this.WindowState)
                {
                    mynotifyicon.Visible = false;
                }
            }
            else
            {

            }
        }

        private void closeapp(object sender, EventArgs e)
        {
            if (isLoggedIn)
            {
                DialogResult result = MessageBox.Show("Do you want to punch out before exiting?", "Punch Out Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    punchout();
                }
                if (result == DialogResult.No) return;
            }
            Application.Exit();
        }


        private void frmdashboard_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                mynotifyicon.Visible = true;
                mynotifyicon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                mynotifyicon.Visible = false;
            }
        }
        private void mynotifyicon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            mynotifyicon.Visible = false;
        }
        public void startup()
        {
            if (System.IO.File.Exists(Application.StartupPath + "\\systemdata"))
            {
                string[] lines1 = System.IO.File.ReadAllLines(Application.StartupPath + "\\systemdata");
                string encryptdata = lines1[0].ToString();
                var base64EncodedBytes = System.Convert.FromBase64String(encryptdata);
                var stringdata = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                var objresult = JsonConvert.DeserializeObject<Users>(stringdata);
                if (objresult != null)
                {
                    txtusername.Text = objresult.Email;
                    txtpassword.Text = objresult.Password;
                    logincheck(false);
                }
            }
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtusername.Text == "")
            {
                MessageBox.Show("Please Enter the UserName", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (txtpassword.Text == "")
            {
                MessageBox.Show("Please Enter the Password", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            logincheck(true);
        }
        public void logoutprocesss()
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            pnllogin.Visible = true;
            Program.Loginlist = new Users();
            Program.token = "";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "\\systemdata"))
            {
                string data = JsonConvert.SerializeObject(Program.Loginlist);
                string data1 = "";
                using (var sha256 = SHA256.Create())
                {
                    var saltedPasswordAsBytes = Encoding.UTF8.GetBytes(data);
                    data1 = Convert.ToBase64String(saltedPasswordAsBytes);
                    sw.WriteLine(data1);
                    sw.Dispose();
                }
            }
            lblname.Text = Program.Loginlist.First_Name;
            lblemail.Text = Program.Loginlist.Email;
            lblshortname.Text = ((Program.Loginlist.First_Name != "" && Program.Loginlist.First_Name != null) ? Program.Loginlist.First_Name[0].ToString() : "") + ((Program.Loginlist.Last_Name != "" && Program.Loginlist.Last_Name != null) ? Program.Loginlist.Last_Name[0].ToString() : "");
        }
        public void logincheck(bool errorshow)
        {
            try
            {
                LoginModels LM = new LoginModels
                {
                    UserName = txtusername.Text,
                    Password = txtpassword.Text
                };

                string master = JsonConvert.SerializeObject(LM);
                string URL = Program.OnlineURL + "api/Login/UserLogin";
                string DATA = master;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.Timeout = TimeSpan.FromMinutes(30);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpContent content = new StringContent(DATA, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(URL, content).Result;

                    string responseString = response.Content.ReadAsStringAsync().Result;

                    Logger.LogInfo("Raw response: " + responseString);

                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            var objresult = JsonConvert.DeserializeObject<loginresult>(responseString);
                            Program.Loginlist = objresult.user;
                            Program.token = objresult.token;
                            isLoggedIn = true;
                            pnllogin.Visible = false;
                            loginprocesss();
                        }
                        catch (JsonReaderException ex)
                        {
                            Logger.LogError("Failed to parse the response. The response was not in the expected JSON format: " + ex.Message);
                            MessageBox.Show("Failed to parse the response. The response was not in the expected JSON format.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        Logger.LogError("Login Error : Code : " + response.StatusCode.ToString());
                        Logger.LogError(responseString);

                        if (errorshow)
                        {
                            try
                            {
                                var errorResult = JsonConvert.DeserializeObject<dynamic>(responseString);
                                MessageBox.Show((string)errorResult.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (JsonReaderException)
                            {
                                MessageBox.Show("Failed to parse the error response. The response was not in the expected JSON format.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("An unexpected error occurred: " + ex.Message);
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loginprocesss()
        {
            try
            {
                timer3.Start();

                string filePath = Path.Combine(Application.StartupPath, "systemdata");

                using (var sw = new System.IO.StreamWriter(filePath))
                {
                    string data = JsonConvert.SerializeObject(Program.Loginlist);
                    string encryptedData = "";

                    using (var sha256 = SHA256.Create())
                    {
                        byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(data);
                        encryptedData = Convert.ToBase64String(saltedPasswordAsBytes);
                        sw.WriteLine(encryptedData);
                    }
                }

                // Update UI elements
                lblname.Text = Program.Loginlist.First_Name;
                lblemail.Text = Program.Loginlist.Email;
                lblshortname.Text = $"{Program.Loginlist.First_Name?.FirstOrDefault()}{Program.Loginlist.Last_Name?.FirstOrDefault()}";
                Lastsync = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogError("An unexpected error occurred in loginprocesss: " + ex.Message);
                MessageBox.Show("An unexpected error occurred during the login process: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void logoutcheck(bool errorshow)
        {
            LoginModels LM = new LoginModels();
            LM.UserName = txtusername.Text;
            LM.Password = txtpassword.Text;
            string master = JsonConvert.SerializeObject(LM);
            string URL = Program.OnlineURL + "api/Login/UserLogout";
            string DATA = master;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
            client1B.BaseAddress = new System.Uri(URL);
            client1B.Timeout = TimeSpan.FromMinutes(30);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            var objresult = JsonConvert.DeserializeObject<loginresult>(responseString1B);
            if (messge1B.IsSuccessStatusCode)
            {
                logoutprocesss();

            }
            else
            {
                Logger.LogError("Logout Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
                if (errorshow == true)
                {
                    MessageBox.Show(objresult.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnbegin_Click(object sender, EventArgs e)
        {
            if (currenttype == 0)
            {
                punchin();
            }
            else if (currenttype == 1)
            {
                punchout();
            }
            else if (currenttype == 2)
            {
                //PunchBreakOut(breakid);
                PunchBreakOut(BreakInfo.Id);
            }
        }

        private DateTime GetISTTime()
        {
            TimeZoneInfo istTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime utcTime = DateTime.UtcNow;
            DateTime istTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, istTimeZone);
            return istTime;
        }

        public void punchin()
        {
            List<UserAttendanceModel> obj = new List<UserAttendanceModel>();
            UserAttendanceModel LM = new UserAttendanceModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            //LM.AttendanceDate = DateTime.Now.ToShortDateString();
            DateTime istTime = GetISTTime();
            LM.AttendanceDate = istTime; //.ToShortDateString();
            //LM.Start_Time = DateTime.Now.ToString();
            LM.Start_Time = istTime; //.ToString();
            LM.End_Time = null;
            LM.Late_Time = null;
            LM.Total_Time = null;
            LM.Status = currenttype;
            obj.Add(LM);
            string master = JsonConvert.SerializeObject(obj);
            string URL = Program.OnlineURL + "api/Users/InsertAttendance";
            string DATA = master;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
            client1B.BaseAddress = new System.Uri(URL);
            client1B.Timeout = TimeSpan.FromMinutes(30);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            if (messge1B.IsSuccessStatusCode)
            {
                currenttype = 1;
                btnbreak.Visible = true;
                SW.Start();
                timer1.Start();
                changestatus();

            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
            }
        }
        public void punchout()
        {
            List<UserAttendanceModel> obj = new List<UserAttendanceModel>();
            UserAttendanceModel LM = new UserAttendanceModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            DateTime istTime = GetISTTime();
            LM.AttendanceDate = istTime; //.ToShortDateString();
            LM.Start_Time = null;
            LM.End_Time = istTime; //.ToString();
            LM.Late_Time = null;
            LM.Total_Time = null;
            LM.Status = currenttype;
            obj.Add(LM);
            string master = JsonConvert.SerializeObject(obj);
            string URL = Program.OnlineURL + "api/Users/InsertAttendance";

            string DATA = master;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
            client1B.BaseAddress = new System.Uri(URL);
            client1B.Timeout = TimeSpan.FromMinutes(30);
            // client1B.DefaultRequestHeaders.Add("Idlist", UpdateIdList);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            if (messge1B.IsSuccessStatusCode)
            {
                currenttype = 0;
                btnbreak.Visible = false;
                SW.Stop();
                SW.Reset();
                SW1.Stop();
                SW1.Reset();
                timer1.Stop();
                changestatus();

                //Application.Exit();

            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
            }
        }
        public async Task PunchBreakIn(int BreakEntryId)
        {
            List<UserBreakModel> obj = new List<UserBreakModel>();
            UserBreakModel LM = new UserBreakModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            DateTime istTime = GetISTTime();
            LM.BreakDate = istTime;//.ToString();
            LM.Start_Time = istTime;//.ToString();
            LM.BreakEntryId = BreakEntryId;
            LM.End_Time = null;
            LM.Status = currenttype;
            obj.Add(LM);
            string master = JsonConvert.SerializeObject(obj);
            string URL = Program.OnlineURL + "api/Users/InsertBreak";
            string DATA = master;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
            client1B.BaseAddress = new System.Uri(URL);
            client1B.Timeout = TimeSpan.FromMinutes(30);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            if (messge1B.IsSuccessStatusCode)
            {
                currenttype = 2;
                //SW.Stop();
                SW1.Start();
                //timer1.Stop();
                changestatus();

                BreakInfo breakDetails = await GetBreakDetails(BreakEntryId);
                if (breakDetails != null)
                {
                    BreakTimerForm breakTimerForm = new BreakTimerForm(this, BreakEntryId, breakDetails.Max_Break_Time, breakDetails.Name);
                    breakTimerForm.ShowDialog();
                }
                else
                {
                    Logger.LogError("Failed to retrieve break details.");
                }
            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
            }
        }
        public void PunchBreakOut(int breakEntryId)
        {
            DateTime istTime = GetISTTime();
            List<UserBreakModel> obj = new List<UserBreakModel>();
            UserBreakModel breakModel = new UserBreakModel
            {
                Id = 0,
                UserId = Program.Loginlist.Id,
                OrganizationId = Program.Loginlist.OrganizationId,
                BreakDate = istTime, //.ToString(),
                Start_Time = istTime, //.ToString(),
                End_Time = istTime,//.ToString(),
                BreakEntryId = breakEntryId,
                Status = 2
            };
            obj.Add(breakModel);
            string master = JsonConvert.SerializeObject(obj);
            string url = Program.OnlineURL + "api/Users/InsertBreak";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.Timeout = TimeSpan.FromMinutes(30);
                client.DefaultRequestHeaders.Add("Authorization", Program.token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(master, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    currenttype = 1;
                    //SW.Start();
                    SW1.Stop();
                    //timer1.Start();
                    changestatus();
                }
                else
                {
                    Logger.LogError("Error: " + response.StatusCode.ToString());
                    Logger.LogError(responseString);
                }
            }
        }
        private async Task<BreakInfo> GetBreakDetails(int breakEntryId)
        {
            string URL = $"{Program.OnlineURL}api/Users/GetBreakMasterById/{breakEntryId}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.Timeout = TimeSpan.FromMinutes(1);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", Program.token);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        BreakInfo breakInfo = JsonConvert.DeserializeObject<BreakInfo>(responseString);

                        return new BreakInfo
                        {
                            Max_Break_Time = breakInfo.Max_Break_Time,
                            Name = breakInfo.Name
                        };
                    }
                    else
                    {
                        Logger.LogError($"Failed to retrieve break details: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Exception occurred while retrieving break details: {ex.Message}");
                    return null;
                }
            }
        }

        public void screenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    byte[] imagedata = ms.ToArray();
                    uploadscreenshot(imagedata);
                }
            }

            if (timer2.Enabled == false)
            {
                timer2.Start();
            }
        }

        public void uploadscreenshot(byte[] imagedata)
        {
            LoginModels LM = new LoginModels
            {
                UserName = txtusername.Text,
                Password = txtpassword.Text
            };

            // Prepare the filename based on current timestamp
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";

            string URL = Program.OnlineURL + "api/Users/UploadFile";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client1B = new HttpClient())
            {
                client1B.BaseAddress = new Uri(URL);
                client1B.Timeout = TimeSpan.FromMinutes(30);
                client1B.DefaultRequestHeaders.Add("UId", Program.Loginlist.Id.ToString());
                client1B.DefaultRequestHeaders.Add("OId", Program.Loginlist.OrganizationId.ToString());
                client1B.DefaultRequestHeaders.Add("SDate", DateTime.Now.ToString());
                client1B.DefaultRequestHeaders.Add("SType", "ScreenShots");
                client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
                client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content1A = new MultipartFormDataContent();

                var imageContent = new ByteArrayContent(imagedata);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content1A.Add(imageContent, "MyImages", filename);

                HttpResponseMessage responseMessage = client1B.PostAsync(URL, content1A).Result;
                string responseString = responseMessage.Content.ReadAsStringAsync().Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    Lastsync = DateTime.Now;
                }
                else
                {
                    Logger.LogError("Upload Error : \n " + responseMessage + " \n Code : " + HttpStatusCode.BadRequest.ToString());
                    Logger.LogError(responseString);
                }
            }
        }
        public void changestatus()
        {
            if (currenttype == 0)
            {
                timer1.Stop();
                btnbegin.Text = "Punch In";
            }
            else if (currenttype == 1)
            {
                timer1.Start();
                btnbegin.Text = "Punch Out";
                btnbreak.Text = "Break";
            }
            else if (currenttype == 2)
            {
                //timer1.Start();
                btnbegin.Text = "Punch Out";
                btnbreak.Text = "Resume";
            }
        }
        public void btnbreak_Click(object sender, EventArgs e)
        {
            if (currenttype == 1)
            {
                if (BreakInfo == null)
                {
                    BreakInfo = new BreakInfo();
                }

                BreakInfo.Id = 0;
                frmbreak obj = new frmbreak();
                obj.ShowDialog();

                if (BreakInfo != null && BreakInfo.Id != 0)
                {
                    PunchBreakIn(BreakInfo.Id);
                    timer1.Stop();
                }

                #region commented
                ////breakid = 0;
                //BreakInfo.Id = 0;
                //frmbreak obj = new frmbreak();
                //obj.ShowDialog();
                //if (BreakInfo.Id != 0)
                //{
                //    //PunchBreakIn(breakid);
                //    PunchBreakIn(BreakInfo.Id);
                //    timer1.Stop();
                //}
                #endregion
            }
            else if (currenttype == 2)
            {
                //PunchBreakOut(breakid);
                PunchBreakOut(BreakInfo.Id);
                timer1.Start();
            }
        }


       #region=======Timers==============
        private void timer1_Tick(object sender, EventArgs e)
        {
            screenshot();
            //browserdetails();
        }

        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    if (addlist.Count != 0)
        //    {
        //        for (int i = 0; i < addlist.Count; i++)
        //        {
        //            uploadscreenshot(addlist[i]);
        //        }
        //    }
        //}

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (addlist.Count != 0)
            {
                for (int i = 0; i < addlist.Count; i++)
                {
                    string filePath = addlist[i];

                    byte[] imagedata = File.ReadAllBytes(filePath);

                    uploadscreenshot(imagedata);
                }
            }
        }

        #region timer3_Tick
        //private void timer3_Tick(object sender, EventArgs e)
        //{
        //    lblcurrenttime.Text = DateTime.Now.ToString("ddd dd MMMM yyyy HH:mm:ss");
        //    TimeSpan ts = SW.Elapsed;
        //    lbltimer.Text = String.Format("{0:00}:{1:00}:{2:00}",
        //    ts.Hours, ts.Minutes, ts.Seconds);
        //    TimeSpan sync = (Lastsync - DateTime.Now);
        //    if (sync.TotalMinutes > 60)
        //    {
        //        diff = String.Format("{1} hours {2} minutes", sync.Hours, sync.Minutes);
        //    }
        //    else
        //    {
        //        diff = sync.Minutes + " mins";
        //    }

        //    lbllastsync.Text = "Last Sync " + diff + " Ago";
        //}
        #endregion

        private void timer3_Tick(object sender, EventArgs e)
        {

            lblcurrenttime.Text = DateTime.Now.ToString("ddd dd MMMM yyyy HH:mm:ss");

            TimeSpan ts = SW.Elapsed;
            lbltimer.Text = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);

            TimeSpan sync = DateTime.Now - Lastsync;

            if (sync.TotalMinutes < 0)
            {
                lbllastsync.Text = "Last Sync: Future time detected";
                return;
            }


            string diff;
            if (sync.TotalMinutes > 60)
            {
                diff = String.Format("{0} hours {1} minutes", sync.Hours, sync.Minutes);
            }
            else
            {
                diff = sync.Minutes + " mins";
            }

            lbllastsync.Text = "Last Sync " + diff + " Ago";
        }

        #endregion

        private void btnlogout_Click(object sender, EventArgs e)
        {
            logoutcheck(true);
        }
    }
}
