﻿using EMP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMP
{
    static class Program
    {
        public static string OnlineURL = "https://localhost:44318/";
        //public static string OnlineURL = "http://3.111.144.69:8080/";
        //public static string OnlineURL = "http://13.201.135.188:8080/";

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
