using Inventory.config;
using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class Sup_edit : Form
    {
        protected String _db_conn = "";
        protected string _id = "";
        protected string _name = "";
        protected string _hp = "";
        protected string _address = "";
        protected string _des = "";

        public Sup_edit(String _id)
        {
            InitializeComponent();
            this._id = _id;
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Sup_edit_Load(object sender, EventArgs e)
        {

            getSupp(_id);

            name.Text = _name;
            HP.Text = _hp;
            almt.Text = _address;
            des.Text = _des;


        }

        private void getSupp(string selectedID)
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "SELECT * FROM suppliers WHERE id = @id";

            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", selectedID);
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _name = reader["supplier_name"].ToString();
                            _hp = reader["phone_numvber"].ToString();
                            _address = reader["address"].ToString();
                            _des = reader["description"].ToString();

                        }
                        reader.Close();
                        connection.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void store()
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "UPDATE `suppliers` SET `supplier_name`=@name,`phone_numvber`=@hp,`address`=@address,`description`=@des WHERE id=@id";

            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", _id);
                    command.Parameters.AddWithValue("@name", name.Text);
                    command.Parameters.AddWithValue("@hp", HP.Text);
                    command.Parameters.AddWithValue("@address", almt.Text);
                    command.Parameters.AddWithValue("@des", des.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Berhasil Update");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            store();
            this.Close();
        }
    }
}
