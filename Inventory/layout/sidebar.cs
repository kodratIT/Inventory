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
        public sidebar()
        {
            InitializeComponent();
        }

        private void sidebar_Load(object sender, EventArgs e)
        {
            container(new Dashboard());
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
            container(new Dashboard());
        }

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
