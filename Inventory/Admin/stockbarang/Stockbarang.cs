using Inventory.Admin;
using Inventory.Admin.stockbarang;
using Inventory.Admin.suppliers;
using Inventory.config;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Stockbarang : Form
    {
        protected String _db_conn = "";
        protected string selectedID = "";
        public Stockbarang()
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Stockbarang_Load(object sender, EventArgs e)
        {
            viewData();
            statistik("SELECT COUNT(*) AS result FROM products",0);
            statistik(" SELECT product_name AS result FROM products WHERE stock = (SELECT MIN(stock) FROM products)", 1);
            statistik(" SELECT product_name AS result FROM products WHERE stock = (SELECT MAX(stock) FROM products)", 2);

        }


        private void statistik(string query,int data)
        {
            using (MySqlConnection connection = new MySqlConnection(_db_conn))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            total.Text = (data == 0) ? reader["result"].ToString() : total.Text;
                            low.Text = (data == 1) ? reader["result"].ToString() : low.Text;
                            hight.Text = (data == 2) ? reader["result"].ToString() : hight.Text;

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

        // Buat koneksi ke database

        private void viewData()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT products.product_id, suppliers.supplier_name,products.product_name,products.stock,products.price FROM `products` \r\nJOIN suppliers ON products.supplier_id = suppliers.id";
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

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            Brg_add brg_add = new Brg_add();
            brg_add.ShowDialog();
            viewData();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
                selectedID = gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void btn_hps_Click(object sender, EventArgs e)
        {

            if (selectedID == "")
            {
                MessageBox.Show("Pilih Supplier Terlebih Dahulu!");
            }
            else
            {
                string query = "DELETE  FROM products WHERE product_id=@product_id";

                using (MySqlConnection connection = new MySqlConnection(_db_conn))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@product_id", selectedID);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("DATA BERHASIL DI HAPUS");
                            connection.Close();
                    
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            viewData();


        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (selectedID == "")
            {
                MessageBox.Show("Pilih Supplier Terlebih Dahulu!");
            }
            else
            {
                Brg_edit edit1 = new Brg_edit(selectedID);
                edit1.ShowDialog();
                viewData();
            }    
        }

        public string getID()
        {
            return selectedID;
        }
    }
}
