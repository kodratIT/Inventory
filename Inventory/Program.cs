﻿using Inventory.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
              Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new brg_out());
            Application.Run(new Login());
            // if(islogin.IsLoggedIn() == true) {
            //    Application.Run(new sidebar());
            //  }
            // else
            // {
            //    Application.Run(new Login());
            //    if(islogin.IsLoggedIn() == true)
            //   {
            //          Application.Exit();
            //      Application.Run(new sidebar());
            //   }
            //   }


        }
    }
}
