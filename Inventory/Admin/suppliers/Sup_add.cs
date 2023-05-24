using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Admin.suppliers
{
    public partial class Sup_add : Form
    {
        protected String Mysql = "server=localhost;database=toko1;uid=root;pwd=\"\"";
        public Sup_add()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            store();
        }
        private void store()
        {
           
            MySqlConnection cnn = new MySqlConnection(Mysql);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "INSERT INTO suppliers VALUES (NULL,@name,@hp,@alamat,@des)";
                cmd.Parameters.AddWithValue("@name", Name.Text);
                cmd.Parameters.AddWithValue("@hp", HP.Text);
                cmd.Parameters.AddWithValue("@alamat", Alamat.Text);
                cmd.Parameters.AddWithValue("@des", des.Text);
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Input data berhasil" + Name.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
