using Inventory.Admin;
using Inventory.Admin.suppliers;
using Inventory.config;
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
    public partial class supplier : Form
    {
        protected String _db_conn = "";
        protected string selectedID = "";
        public supplier()
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Stockbarang_Load(object sender, EventArgs e)
        {
            viewData();
            
        }

        private void viewData()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT * FROM suppliers";
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
                
        }   

        private void btn_hps_Click(object sender, EventArgs e)
        {
            if (selectedID == "")
            {
                MessageBox.Show("Pilih Supplier Terlebih Dahulu!");
            }else
            {
                // Query SQL untuk mengambil data dari tabel
                string query = "DELETE  FROM suppliers WHERE id=@id";

                using (MySqlConnection connection = new MySqlConnection(_db_conn))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", selectedID);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("DATA BERHASIL DI HAPUS ");
                            connection.Close();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (selectedID == "")
            {
                MessageBox.Show("Pilih Supplier Terlebih Dahulu!");
            }else
            {
                Sup_edit edit = new Sup_edit(selectedID);
                edit.ShowDialog();
            }
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            Supplier_add ad = new Supplier_add();
            ad.ShowDialog();
        }

        public string getID()
        {
            return selectedID;
        }
    }
}
