using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Admin
{
    public partial class sidebar : Form
    {
        protected string user_id;
        public sidebar(string _id)
        {
            InitializeComponent();
            this.user_id = _id;
        }

        private void sidebar_Load(object sender, EventArgs e)
        {
            container(new Dashboard(user_id));
        }

        private void gunaPanelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            container(new Dashboard(user_id));
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            container(new rwyt_brg_in(user_id));
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            container(new Stockbarang());
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            container(new his_out(user_id));
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            container(new supplier());
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            container(new Store());
        }

        private void gunaButton7_Click_1(object sender, EventArgs e)
        {
            Login login = new Login();
            login.setIsLoggedIn(false);
            if (login.IsLoggedIn() != true)
            {
                this.Hide();
                login.ShowDialog();
            }
        }
    }
}
