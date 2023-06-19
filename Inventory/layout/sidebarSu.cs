using System;
using Inventory.Admin.master;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.layout
{
    public partial class sidebarSu : Form
    {
        protected string user_id;
        public sidebarSu(string _id)
        {
            InitializeComponent();
            this.user_id = _id;
        }


        private void container(object _form)
        {

            if (gunaPanelContainer.Controls.Count > 0) gunaPanelContainer.Controls.Clear();

            Form fm = _form as Form;
            fm.TopLevel = false;
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.Dock = DockStyle.Fill;
            gunaPanelContainer.Controls.Add(fm);
            gunaPanelContainer.Tag = fm;
            fm.Show();

        }

        private void sidebarSu_Load(object sender, EventArgs e)
        {
            container(new Dashboard(user_id));
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            container(new Dashboard(user_id));
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            container(new master());
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.setIsLoggedIn(false);
            if(login.IsLoggedIn() != true)
            {
                this.Hide();
                login.ShowDialog();
            }
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            container(new Dashboard(user_id));
        }
    }
}
