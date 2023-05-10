using MySql.Data.MySqlClient;
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
        protected String _db_conn = "server=localhost;database=toko1;uid=root;pwd=\"\"";

        public Stockbarang()
        {
            InitializeComponent();
        }

        private void Stockbarang_Load(object sender, EventArgs e)
        {
            viewData();
            btnStock.BackColor= Color.Blue;
            
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
            dataTabel.DataSource = dataTable;
        }


      
    }
}
