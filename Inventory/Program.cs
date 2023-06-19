using Inventory.Admin;
using Inventory.layout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventory.config;


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
            
            string id;
            string role;
            Boolean isloggin=false;
            Login login = new Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                id = login.getID();
                role = login.getRole();
                isloggin = login.IsLoggedIn();

                if (role == "0")
                {
                    Application.Run(new sidebar(id));
                }else if(role == "1")
                {
                    Application.Run(new sidebarSu(id));
                }
                else
                {
                    Application.Run(new Login());
                }
            }
          
        }
    }
}
