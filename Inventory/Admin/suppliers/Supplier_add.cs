using Inventory.config;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections;
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
    public partial class Supplier_add : Form
    {
        private string _Mysql = "";
        public Supplier_add()
        {
            InitializeComponent();
            Database db = new Database();
            _Mysql= db.MysqlConn();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if(gunaName.Text== "" || hp.Text == "" || address.Text == "" || des.Text == ""){
                MessageBox.Show("Isi Data Terlebih Dahulu!");
            }
            else
            {
                store();
            }
        }

        private void store()
        {
            string query = "INSERT INTO suppliers (id,supplier_name,phone_numvber,address,description) VALUES(NULL,@name,@hp,@address,@des); ";
            using (MySqlConnection connection = new MySqlConnection(_Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name",gunaName.Text );
                    command.Parameters.AddWithValue("@hp",hp.Text );
                    command.Parameters.AddWithValue("@address",address.Text );
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

        private void gunaControlBox1_Click(object sender, EventArgs e)
        {

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
