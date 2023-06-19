using Inventory.config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Inventory.Admin.store
{

    public partial class Store_edit : Form
    {
        protected String _db_conn = "";
        protected string _id = "";
        protected string _name = "";
        protected string _hp = "";
        protected string _address = "";
        protected string _des = "";

        public Store_edit(string _id)
        {
            InitializeComponent();
            this._id = _id;
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Store_edit_Load(object sender, EventArgs e)
        {
            getStore(_id);

            name.Text = _name;
            HP.Text = _hp;
            almt.Text = _address;
            des.Text = _des;
        }

        private void getStore(string selectedID)
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "SELECT * FROM stores WHERE id = @id";
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
                            _name = reader["store_name"].ToString();
                            _hp = reader["phone_number"].ToString();
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
            string query = "UPDATE `stores` SET `store_name`=@name,`phone_number`=@hp,`address`=@address,`description`=@des WHERE id=@id";

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

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            store();
            this.Close();
        }
    }
}
