using Inventory.Admin;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public partial class store : Form
    {
        protected String _db_conn = "server=localhost;database=toko1;uid=root;pwd=\"\"";
        protected string selectedID = "";
        public store()
        {
            InitializeComponent();
        }

        private void Stockbarang_Load(object sender, EventArgs e)
        {
            viewData();
            //btnStock.BackColor= Color.Blue;
            
        }

        // Buat koneksi ke database

        private void viewData()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT * FROM products";
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
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
                selectedID = gunaDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                //textBoxNama.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                //textBoxNIM.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                //textBoxNotelepon.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                //textBoxAlamat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btn_hps_Click(object sender, EventArgs e)
        {
            // Query SQL untuk mengambil data dari tabel
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

        private void gunaButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
