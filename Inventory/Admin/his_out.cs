using Guna.UI.WinForms;
using Inventory.config;
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

namespace Inventory.Admin
{
    public partial class his_out : Form
    {
        protected String _db_conn = "";
        int showEntry =0;
        protected string user_id;

        public his_out(string user_id)
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
            this.user_id= user_id;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {

        }

        private void rwyt_brg_in_Load(object sender, EventArgs e)
        {
            DataView();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Brg_In brgin = new Brg_In(user_id);
            brgin.ShowDialog();          
        }

        private void DataView(string searchQuery = "")
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);

            // Ambil jumlah entri yang diinginkan dari GunaComboBox
            if (cmbShowEntry.SelectedItem != null)
            {
                showEntry = int.Parse(cmbShowEntry.SelectedItem.ToString());
            }

            // Buat perintah SQL untuk mengambil data dari tabel dengan filter pencarian
            string sql = "SELECT out_transaction.out_transaction_id AS TransactionId,users.name AS Admin,stores.store_name AS Store,products.product_name AS Product,create_at,out_detail.qty AS qty FROM out_transaction JOIN stores ON out_transaction.store_id = stores.id JOIN users ON out_transaction.user_id = users.id JOIN out_detail ON out_transaction.out_transaction_id = out_detail.out_transaction_id JOIN products ON out_detail.product_id = products.product_id";

            // Tambahkan kondisi WHERE jika ada pencarian
            if (!string.IsNullOrEmpty(searchQuery))
            {
                sql += " WHERE product_name LIKE @searchQuery OR store_name LIKE @searchQuery OR store_name LIKE @searchQuery OR name LIKE @searchQuery";
            }

            sql += " ORDER BY create_at DESC";

            // Tambahkan klausul LIMIT untuk jumlah entri yang diinginkan

            if (showEntry != 0)
            {
                sql += " LIMIT @showEntry";
            }

            MySqlCommand command = new MySqlCommand(sql, cnn);


            // Tambahkan parameter pencarian jika ada
            if (!string.IsNullOrEmpty(searchQuery))
            {
                command.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
            }

            // Tambahkan parameter showEntry
            if (showEntry  != 0)
            {
                command.Parameters.AddWithValue("@showEntry", showEntry);
            }


            // Buat objek DataAdapter dan DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            // Isi data dari database ke dalam DataTable
            adapter.Fill(dataTable);

            // Tampilkan data dari DataTable ke dalam DataGridView
            gunaDataGridView2.DataSource = dataTable;
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
            brg_out brgout = new brg_out(user_id);
            brgout.ShowDialog();
        }

        private void gunaButton2_Click_1(object sender, EventArgs e)
        {
            string searchQuery = text.Text;

            DataView(searchQuery);
        }

        private void cmbShowEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView();
        }
    }
}


