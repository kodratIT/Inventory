using Guna.UI.WinForms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
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
    public partial class rwyt_brg_in : Form
    {
        protected String _db_conn = "server=localhost;database=toko1;uid=root;pwd=\"\"";

        public rwyt_brg_in()
        {
            InitializeComponent();
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
            Brg_In brgin = new Brg_In();
            brgin.ShowDialog();          
        }

        private void DataView()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT in_transaction.id_transaction AS ID_Transcation,products.product_name AS Product,suppliers.supplier_name AS supplier,in_detail.qty,in_transaction.create_at FROM in_transaction JOIN in_detail ON in_transaction.id_transaction = in_detail.transaction_in_id  JOIN products ON in_detail.product_id = products.product_id JOIN suppliers ON suppliers.id = products.supplier_id ORDER BY create_at DESC";
                MySqlCommand command = new MySqlCommand(sql, cnn);

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
            Brg_In brgin = new Brg_In();
            brgin.ShowDialog();
        }

        private void gunaLabel5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaLineTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLineTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaLabel9_Click(object sender, EventArgs e)
        {

        }

        private void gunaLineTextBox4_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}


