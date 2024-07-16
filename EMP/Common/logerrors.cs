using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace EMP
{

    public class LogErrors
    {


        public LogErrors()
        {
          
        }

        public void Writelog(Exception exception, string methodName, string reqModel = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                //gets file name and path
                string fileName = DateTime.Today.ToString("dd-MMM-yy") + ".txt";
                string webRootPath = Application.StartupPath;
                string filePath = Path.Combine(webRootPath, "Logs/" + fileName);

                //create directory if not exists
                if (!Directory.Exists(Path.Combine(webRootPath, "Logs")))
                {
                    Directory.CreateDirectory(Path.Combine(webRootPath, "Logs"));
                }

                //content to write
                sb.Append("Log Entry Date : " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + Environment.NewLine);
                sb.Append("Api Method Name : " + methodName + Environment.NewLine);
                sb.Append("Request model : " + reqModel + Environment.NewLine);
                sb.Append("Exception Message : " + exception.Message + Environment.NewLine);

                if (exception.InnerException != null)
                    sb.Append("Inner Exception : " + exception.InnerException.Message + Environment.NewLine);

                //create file if not exists
                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        sb.Append("__________________________");
                        byte[] content = new UTF8Encoding(true).GetBytes(sb.ToString());
                        fs.Write(content, 0, content.Length);
                    }
                }
                //write to existing file if exists
                else
                {
                    StreamWriter w = File.AppendText(filePath);
                    w.WriteLine("\r\n" + sb.ToString());
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void WriteDirectLog(string log, string reqModel = "")
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                //gets file name and path
                string fileName = DateTime.Today.ToString("dd-MMM-yy") + ".txt";
                string webRootPath = Application.StartupPath;
                string filePath = Path.Combine(webRootPath, "Logs/" + fileName);

                //create directory if not exists
                if (!Directory.Exists(Path.Combine(webRootPath, "Logs")))
                {
                    Directory.CreateDirectory(Path.Combine(webRootPath, "Logs"));
                }

                //content to write
                sb.Append("Log Entry Date : " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + Environment.NewLine);
                sb.Append("Log : " + log + Environment.NewLine);
                sb.Append("Request model : " + reqModel + Environment.NewLine);
                //create file if not exists
                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        sb.Append("__________________________");
                        byte[] content = new UTF8Encoding(true).GetBytes(sb.ToString());
                        fs.Write(content, 0, content.Length);
                    }
                }
                //write to existing file if exists
                else
                {
                    StreamWriter w = File.AppendText(filePath);
                    w.WriteLine("\r\n" + sb.ToString());
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
