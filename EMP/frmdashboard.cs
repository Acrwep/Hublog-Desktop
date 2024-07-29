using EMP.Models;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class frmdashboard : Form
    {
        public frmdashboard()
        {
            InitializeComponent();
        }

        public int currenttype = 0;
        public static int breakid = 0;
        public string diff = "";
        Stopwatch SW = new Stopwatch();
        Stopwatch SW1 = new Stopwatch();
        DateTime Lastsync = new DateTime();

        public List<string> addlist = new List<string>();

        //0 none
        //1 punch
        //2 break
        //3 out
        private void frmdashboard_Load(object sender, EventArgs e)
        {
            pnllogin.Location = new Point() { X = 12, Y = 12 };
            this.Size = new Size() { Width = 374, Height = 471 };
            this.mynotifyicon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.mynotifyicon.ContextMenuStrip.Items.Add("QUIT", null, closeapp);
            timer3.Start();
            startup();
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
        public void loginprocesss()
        {
            timer3.Start();
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
            Lastsync = DateTime.Now;
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
            LoginModels LM = new LoginModels();
            LM.UserName = txtusername.Text;
            LM.Password = txtpassword.Text;
            string master = JsonConvert.SerializeObject(LM);
            string URL = Program.OnlineURL + "api/Login/UserLogin";
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

                Program.Loginlist = objresult.user;
                Program.token = objresult.token;
                pnllogin.Visible = false;
                loginprocesss();

            }
            else
            {

                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
                if (errorshow == true)
                {
                    MessageBox.Show(objresult.message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //var objmaster = JsonConvert.DeserializeObject<ErrorMsg>(responseString1B);
                //Logger.LogError("Login Server Error");
                //Logger.LogError(objmaster.Message);
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
                PunchBreakOut(breakid);
            }
        }
        public void punchin()
        {
            List<UserAttendanceModel> obj = new List<UserAttendanceModel>();
            UserAttendanceModel LM = new UserAttendanceModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            LM.AttendanceDate = DateTime.Now.ToShortDateString();
            LM.Start_Time = DateTime.Now.ToString();
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
                //var objmaster = JsonConvert.DeserializeObject<ErrorMsg>(responseString1B);
                //Logger.LogError("Login Server Error");
                //Logger.LogError(objmaster.Message);
            }
        }
        public void punchout()
        {
            List<UserAttendanceModel> obj = new List<UserAttendanceModel>();
            UserAttendanceModel LM = new UserAttendanceModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            LM.AttendanceDate = DateTime.Now.ToShortDateString();
            LM.Start_Time = null;
            LM.End_Time = DateTime.Now.ToString();
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



            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
                //var objmaster = JsonConvert.DeserializeObject<ErrorMsg>(responseString1B);
                //Logger.LogError("Login Server Error");
                //Logger.LogError(objmaster.Message);
            }
        }
        public void PunchBreakIn(int BreakEntryId)
        {
            List<UserBreakModel> obj = new List<UserBreakModel>();
            UserBreakModel LM = new UserBreakModel();
            LM.Id = 0;
            LM.UserId = Program.Loginlist.Id;
            LM.OrganizationId = Program.Loginlist.OrganizationId;
            LM.BreakDate = DateTime.Now.ToString();
            LM.Start_Time = DateTime.Now.ToString();
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
            // client1B.DefaultRequestHeaders.Add("Idlist", UpdateIdList);
            client1B.DefaultRequestHeaders.Add("Name", "");
            client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
            client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage messge1B = client1B.PostAsync(URL, content1B).Result;
            var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
            if (messge1B.IsSuccessStatusCode)
            {
                currenttype = 2;
                SW.Stop();
                SW1.Start();
                timer1.Stop();
                changestatus();

            }
            else
            {
                Logger.LogError("Login Error : Code : " + HttpStatusCode.BadRequest.ToString());
                Logger.LogError(responseString1B);
            }
        }
        public void PunchBreakOut(int breakEntryId)
        {
            List<UserBreakModel> obj = new List<UserBreakModel>();
            UserBreakModel breakModel = new UserBreakModel
            {
                Id = 0,
                UserId = Program.Loginlist.Id,
                OrganizationId = Program.Loginlist.OrganizationId,
                BreakDate = DateTime.Now.ToString(),
                Start_Time = DateTime.Now.ToString(),
                End_Time = DateTime.Now.ToString(),
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
                    SW.Start();
                    SW1.Stop();
                    timer1.Start();
                    changestatus();
                }
                else
                {
                    Logger.LogError("Error: " + response.StatusCode.ToString());
                    Logger.LogError(responseString);
                }
            }
        }

        #region  commented old uploadscreenshot
        //public void uploadscreenshot(string path)
        //{
        //    LoginModels LM = new LoginModels();
        //    LM.UserName = txtusername.Text;
        //    LM.Password = txtpassword.Text;
        //    string filename = Path.GetFileName(path);
        //    Image im = Image.FromFile(path);
        //    byte[] imagedata = null;
        //    using (var ms = new MemoryStream())
        //    {
        //        im.Save(ms, im.RawFormat);
        //        imagedata = ms.ToArray();
        //    }
        //    string master = JsonConvert.SerializeObject(LM);
        //    string URL = Program.OnlineURL + "api/Users/UploadFile";
        //    string DATA = master;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    System.Net.Http.HttpClient client1B = new System.Net.Http.HttpClient();
        //    client1B.BaseAddress = new System.Uri(URL);
        //    client1B.Timeout = TimeSpan.FromMinutes(30);
        //    client1B.DefaultRequestHeaders.Add("UId", Program.Loginlist.Id.ToString());
        //    client1B.DefaultRequestHeaders.Add("OId", Program.Loginlist.OrganizationId.ToString());
        //    client1B.DefaultRequestHeaders.Add("SDate", DateTime.Now.ToString());
        //    client1B.DefaultRequestHeaders.Add("SType", "ScreenShots");
        //    client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
        //    client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    System.Net.Http.HttpContent content1B = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");
        //    MultipartFormDataContent content1A = new MultipartFormDataContent();
        //    content1A.Add(new StreamContent(new MemoryStream(imagedata)), "MyImages", filename);
        //    HttpResponseMessage messge1B = client1B.PostAsync(URL, content1A).Result;
        //    var responseString1B = messge1B.Content.ReadAsStringAsync().Result;
        //    im.Dispose();
        //    if (messge1B.IsSuccessStatusCode)
        //    {
        //        Lastsync = DateTime.Now;
        //        File.Delete(path);
        //        addlist.Remove(path);
        //    }
        //    else
        //    {
        //        if (addlist.Where(c => c == path).Count() == 0)
        //        {
        //            addlist.Add(path);
        //        }
        //        Logger.LogError("Upload Error : \n " + messge1B + " \n Code : " + HttpStatusCode.BadRequest.ToString());
        //        Logger.LogError(responseString1B);
        //    }
        //}
        #endregion

        #region  second commented Upload Screenshots
        //public void uploadscreenshot(string path)
        //{
        //    LoginModels LM = new LoginModels
        //    {
        //        UserName = txtusername.Text,
        //        Password = txtpassword.Text
        //    };

        //    string filename = Path.GetFileName(path);
        //    Image im = Image.FromFile(path);
        //    byte[] imagedata;
        //    using (var ms = new MemoryStream())
        //    {
        //        im.Save(ms, im.RawFormat);
        //        imagedata = ms.ToArray();
        //    }

        //    string URL = Program.OnlineURL + "api/Users/UploadFile";
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    using (var client1B = new HttpClient())
        //    {
        //        client1B.BaseAddress = new Uri(URL);
        //        client1B.Timeout = TimeSpan.FromMinutes(30);
        //        client1B.DefaultRequestHeaders.Add("UId", Program.Loginlist.Id.ToString());
        //        client1B.DefaultRequestHeaders.Add("OId", Program.Loginlist.OrganizationId.ToString());
        //        client1B.DefaultRequestHeaders.Add("SDate", DateTime.Now.ToString());
        //        client1B.DefaultRequestHeaders.Add("SType", "ScreenShots");
        //        client1B.DefaultRequestHeaders.Add("Authorization", Program.token);
        //        client1B.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        var content1A = new MultipartFormDataContent();

        //        var imageContent = new ByteArrayContent(imagedata);
        //        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        //        content1A.Add(imageContent, "MyImages", filename);

        //        HttpResponseMessage responseMessage = client1B.PostAsync(URL, content1A).Result;
        //        string responseString = responseMessage.Content.ReadAsStringAsync().Result;

        //        im.Dispose();

        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            Lastsync = DateTime.Now;
        //            File.Delete(path);
        //            addlist.Remove(path);
        //        }
        //        else
        //        {
        //            if (!addlist.Contains(path))
        //            {
        //                addlist.Add(path);
        //            }
        //            Logger.LogError("Upload Error : \n " + responseMessage + " \n Code : " + HttpStatusCode.BadRequest.ToString());
        //            Logger.LogError(responseString);
        //        }
        //    }
        //}
        #endregion

        #region  second commented screenshot
        //public void screenshot()
        //{
        //    Rectangle bounds = Screen.GetBounds(Point.Empty);
        //    string name = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    name = Application.StartupPath + "\\ScreenShot\\" + name + ".jpg";
        //    using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        //    {
        //        using (Graphics g = Graphics.FromImage(bitmap))
        //        {
        //            g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
        //        }
        //        bitmap.Save(name, ImageFormat.Jpeg);
        //    }
        //    addlist.Add(name);

        //    if (timer2.Enabled == false)
        //    {
        //        timer2.Start();
        //    }
        //}
        #endregion



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

        #region screenshot
        public void screenshot()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            string name = DateTime.Now.ToString("yyyyMMddHHmmss");
            //name = Application.StartupPath + "\\ScreenShot\\" + name + ".jpg";
            name = "C:\\Users\\Administrator\\Pictures\\" + "\\ScreenShot\\" + name + ".jpg";
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                bitmap.Save(name, ImageFormat.Jpeg);
            }
            addlist.Add(name);


        #region  Commented Screenshot Interval from API
        //private readonly object listLock = new object();
        //private async Task screenshot() 
        //{
        //    int interval = await GetScreenshotIntervalAsync();

        //    timer1.Interval = interval;

        //    Rectangle bounds = Screen.GetBounds(Point.Empty);
        //    string name = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    string directoryPath = Path.Combine(Application.StartupPath, "ScreenShot");

        //    if (!Directory.Exists(directoryPath))
        //    {
        //        Directory.CreateDirectory(directoryPath);
        //    }

        //    string filePath = Path.Combine(directoryPath, $"{name}.jpg");

        //    using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        //    {
        //        using (Graphics g = Graphics.FromImage(bitmap))
        //        {
        //            g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
        //        }
        //        bitmap.Save(filePath, ImageFormat.Jpeg);
        //    }

        //    lock (listLock)
        //    {
        //        addlist.Add(filePath);
        //    }

        //    if (!timer2.Enabled)
        //    {
        //        timer2.Interval = interval;
        //        timer2.Start();
        //    }
        //    else
        //    {
        //        timer2.Interval = interval;
        //    }
        //}

        //private async Task<int> GetScreenshotIntervalAsync()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(Program.OnlineURL + "api/Screenshot/GetSelectedInterval");
        //        response.EnsureSuccessStatusCode();

        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Response Body: {responseBody}");

        //        try
        //        {
        //            var intervals = JArray.Parse(responseBody);
        //            if (intervals.Count > 0)
        //            {
        //                var intervalInMilliseconds = (int)intervals[0]["intervalInMilliseconds"];
        //                return intervalInMilliseconds;
        //            }

        //            throw new Exception("No intervals found in API response.");
        //        }
        //        catch (JsonException ex)
        //        {
        //            throw new Exception("Error parsing JSON response.", ex);
        //        }
        //    }
        //}
        #endregion

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
        private void btnbreak_Click(object sender, EventArgs e)
        {
            if (currenttype == 1)
            {
                breakid = 0;
                frmbreak obj = new frmbreak();
                obj.ShowDialog();
                if (breakid != 0)
                {
                    PunchBreakIn(breakid);
                    timer1.Stop();
                }
            }
            else if (currenttype == 2)
            {
                PunchBreakOut(breakid);
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

        private void timer3_Tick(object sender, EventArgs e)
        {
            lblcurrenttime.Text = DateTime.Now.ToString("ddd dd MMMM yyyy HH:mm:ss");
            TimeSpan ts = SW.Elapsed;
            lbltimer.Text = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
            TimeSpan sync = (Lastsync - DateTime.Now);
            if (sync.TotalMinutes > 60)
            {
                diff = String.Format("{1} hours {2} minutes", sync.Hours, sync.Minutes);
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
