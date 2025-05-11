using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using F21Party.DBA;
using F21Party.Views;
using F21Party.Controllers;

namespace F21Party
{
    internal static class Program
    {
        public static int UserID;
        public static int UserAccessID;
        public static string UserAccessLevel;
        public static int UserAuthority;
        //public static string TestingMessage;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frm_Main());
        }
    }
}
