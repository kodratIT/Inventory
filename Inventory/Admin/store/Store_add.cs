using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Admin.store
{
    public partial class Store_add : Form
    {
        private string _Mysql = "server=localhost;database=toko1;uid=root;pwd=\"\"";
        public Store_add()
        {
            InitializeComponent();
        }

        private void Store()
        {
            string query = "INSERT INTO `stores`(`id`, `store_name`, `phone_number`, `address`, `description`) VALUES (NULL,@name,@hp,@address,@des)";
            using (MySqlConnection connection = new MySqlConnection(_Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", gunaName.Text);
                    command.Parameters.AddWithValue("@hp", hp.Text);
                    command.Parameters.AddWithValue("@address", address.Text);
                    command.Parameters.AddWithValue("@des", des.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Berhasil Menambahkan Data!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Store();

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            gunaName.Text = "";
            hp.Text = "";
            address.Text = "";
            des.Text = "";
        }
    }
}
