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
using System.Xml.Linq;

namespace Inventory.Admin.master
{
    public partial class master : Form
    {
        protected string selectedID = "";
        protected String _db_conn = "";
        protected int _roleUpdate ;

        public master()
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void gunaLabel4_Click(object sender, EventArgs e)
        {

        }

        private void viewData()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT id,name,email FROM users";
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            MySqlCommand command = new MySqlCommand(sql, cnn);

            // Buat objek DataAdapter dan DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            // Isi data dari database ke dalam DataTable
            adapter.Fill(dataTable);

            // Tampilkan data dari DataTable ke dalam DataGridView
            gunaDataGridView1.DataSource = dataTable;
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedID = gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            seacrhUser(selectedID);
        }

        private void seacrhUser(string _id)
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "SELECT name,email,role FROM users WHERE id = @id";
            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", _id);
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            name.Text = reader["name"].ToString();
                            gunaTextBox2.Text = reader["email"].ToString();
                            string roleValue = reader["role"].ToString();
                            if(roleValue == "0")
                            {
                                gunaTextBox3.Text = "admin";
                            }
                            if(roleValue == "1")
                            {
                                gunaTextBox3.Text = "Super-admin";
                            }
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

        private void master_Load(object sender, EventArgs e)
        {
            viewData();
            gunaComboBox1.SelectedIndex = 0;
        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected item from the combobox
            string selectedItem = gunaComboBox1.SelectedItem.ToString();
            // Perform some action based on the selected item
            if (selectedItem == "Super-admin")
            {
                _roleUpdate = 1;
            }
            else if (selectedItem == "admin")
            {
                _roleUpdate = 0;

            }
            else
            {
                // Do something for other options
            }
        }

        private void store()
        {
            // Query SQL untuk mengambil data dari tabel
            string query = "UPDATE `users` SET `role` =@role WHERE id=@id";

            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", selectedID);
                    command.Parameters.AddWithValue("@role", _roleUpdate);

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
            viewData();
            reset();
        }
        private void reset()
        {
            name.Text = "";
            gunaTextBox2.Text = "";
            gunaTextBox3.Text = "";
            gunaComboBox1.SelectedIndex= 0;
        }
    }
}
