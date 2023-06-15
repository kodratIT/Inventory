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
                            string dump = reader["result"].ToString();
                            br_masuk.Text = (data == 0) ? reader["result"].ToString() : br_masuk.Text;
                            if (data == 1)
                            {
                                br_keluar.Text = string.IsNullOrEmpty(dump) ? "0" : dump;
                            }
                            supp.Text = (data == 2) ? reader["result"].ToString() : supp.Text;
                            st.Text = (data == 3) ? reader["result"].ToString() : st.Text;
                            if (data == 4)
                            {
                                chartMasuk.Text = string.IsNullOrEmpty(dump) ? "0" : dump;
                            }

                            if (data == 5)
                            {
                                chartKeluar.Text = string.IsNullOrEmpty(dump) ? "0" : dump;
                            }
                            if (data == 6)
                            {
                                supbar.Text = string.IsNullOrEmpty(dump) ? "0" : dump;
                            }
                            if (data == 7)
                            {
                                strbar.Text = string.IsNullOrEmpty(dump) ? "0" : dump;
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

        private void Dashboard_Load(object sender, EventArgs e)
        {
            statistik("SELECT SUM(qty) AS result FROM in_detail",0);
            statistik("SELECT SUM(qty) AS result FROM out_detail", 1);
            statistik("SELECT COUNT(id) AS result FROM suppliers", 2);
            statistik("SELECT COUNT(id) AS result FROM stores", 3);
            statistik("SELECT SUM(in_detail.qty) AS result FROM in_transaction LEFT JOIN in_detail ON in_transaction.id_transaction = in_detail.transaction_in_id WHERE in_transaction.create_at >= DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND in_transaction.create_at <= CURDATE()", 4);
            statistik("SELECT SUM(out_detail.qty) AS result FROM out_transaction LEFT JOIN out_detail ON out_transaction.out_transaction_id = out_detail.out_transaction_id WHERE out_transaction.create_at >= DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND out_transaction.create_at <= CURDATE()", 5);
            statistik("SELECT COUNT(id) AS result FROM `suppliers` WHERE created_at >= DATE_SUB(CURDATE(), INTERVAL 7 DAY) AND created_at <= CURDATE()", 6);
            statistik("SELECT COUNT(id) AS result FROM `stores` WHERE created_at >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)\r\nAND created_at <= CURDATE()", 7);

        }
    }
}
