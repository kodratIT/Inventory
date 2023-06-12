using Inventory.config;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Dashboard : Form
    {
        protected string _db_conn = "";
        public Dashboard()
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Stockbarang stockbarang = new Stockbarang();
            stockbarang.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaLabel4_Click(object sender, EventArgs e)
        {

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
                            br_masuk.Text = (data == 0) ? reader["result"].ToString() : br_masuk.Text;
                            br_keluar.Text = (data == 1) ? reader["result"].ToString() : br_keluar.Text;
                            supp.Text = (data == 2) ? reader["result"].ToString() : supp.Text;
                            st.Text = (data == 3) ? reader["result"].ToString() : st.Text;
                            chartMasuk.Text = (data == 4) ? reader["result"].ToString() : chartMasuk.Text;
                            chartKeluar.Text = (data == 5) ? reader["result"].ToString() : chartKeluar.Text;

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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            statistik("SELECT SUM(qty) AS result FROM in_detail",0);
            statistik("SELECT SUM(qty) AS result FROM out_detail", 1);
            statistik("SELECT COUNT(id) AS result FROM suppliers", 2);
            statistik("SELECT COUNT(id) AS result FROM stores", 3);
            statistik("SELECT SUM(in_detail.qty) AS result FROM in_transaction INNER JOIN in_detail ON in_transaction.id_transaction = in_detail.transaction_in_id WHERE in_transaction.create_at >= 2023-06-01 AND in_transaction.create_at <= 2023-06-13", 4);
            statistik("SELECT SUM(out_detail.qty) AS result FROM out_transaction INNER JOIN out_detail ON out_transaction.id = out_detail.out_transaction_id WHERE out_transaction.create_at >= 2023-06-01 AND out_transaction.create_at <= 2023-06-13",5);

        }
    }
}
