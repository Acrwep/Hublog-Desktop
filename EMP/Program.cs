using EMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMP
{
    static class Program
    {
        public static string OnlineURL = "http://43.204.54.223:8001/";
        public static Users Loginlist = new Users();
        public static string token = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmdashboard());
        }
    }
}
