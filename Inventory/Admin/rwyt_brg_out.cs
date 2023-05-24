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
    public partial class rwyt_brg_out : Form
    {
        
        public rwyt_brg_out()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            brg_out brgout = new brg_out();
            brgout.ShowDialog();
        }
    }
}
