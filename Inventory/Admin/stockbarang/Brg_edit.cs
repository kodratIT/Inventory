using Inventory.config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Admin.stockbarang
{
    public partial class Brg_edit : Form
    {
        protected String _db_conn = "";
        protected string _id = "";
        protected string _name = "";
        protected string _price = "";
        public Brg_edit(string _id)
        {
            InitializeComponent();
            this._id = _id;
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Brg_edit_Load(object sender, EventArgs e)
        {
            getSupp(_id);
            name.Text = _name;
            price.Text = _price;
        }

        private void getSupp(string selectedID)
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "SELECT * FROM products WHERE product_id = @id";

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
                            _name = reader["product_name"].ToString();
                            _price = reader["price"].ToString();

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
            string query = "UPDATE `products` SET `product_name`=@name,`price`=@price WHERE product_id=@id";

            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", _id);
                    command.Parameters.AddWithValue("@name", name.Text);
                    command.Parameters.AddWithValue("@price", price.Text);

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
        }
    }
}
