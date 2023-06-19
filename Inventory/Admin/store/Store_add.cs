using Inventory.config;
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
        private string _Mysql = "";
        public Store_add()
        {
            InitializeComponent();
            Database db = new Database();
            _Mysql = db.MysqlConn();
        }

        private void Store()
        {
            string query = "INSERT INTO `stores`(`id`, `store_name`, `phone_number`, `address`, `description`,`created_at`) VALUES (NULL,@name,@hp,@address,@des,@time)";
            DateTime tanggal = DateTime.Now; // Tanggal saat ini

            string tanggalSql = tanggal.ToString("yyyy-MM-dd HH:mm:ss");
            using (MySqlConnection connection = new MySqlConnection(_Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", gunaName.Text);
                    command.Parameters.AddWithValue("@hp", hp.Text);
                    command.Parameters.AddWithValue("@address", address.Text);
                    command.Parameters.AddWithValue("@des", des.Text);
                    command.Parameters.AddWithValue("@time", tanggalSql);


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
            this.Close();

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
