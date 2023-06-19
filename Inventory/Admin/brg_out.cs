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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory.Admin
{
    public partial class brg_out : Form
    {
        protected String Mysql = "";
        protected String selectedOption1 ,selectedOption2 = "";
        protected string selectedStr = "";
        protected string _str_id = "";
        protected String user_id;
        protected string _IDKel, _store_id ="";
        public brg_out(string user_id)
        {
            InitializeComponent();
            Database db = new Database();
            Mysql = db.MysqlConn();

            this.user_id = user_id;
        }


        private List<string> GetDataFromDatabase(string query)
        {
            List<string> data = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string optionName = reader.GetString(0);
                                data.Add(optionName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            return data;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {

        }

        private void gunaComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOption1 = gunaComboBox1.SelectedItem.ToString();
            searchName();
        }

        private void searchName()
        {

            string query = "SELECT product_name FROM products WHERE product_id=@product_id";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", selectedOption1);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            gunaName.Text = reader["product_name"].ToString();
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

        private void getStore()
        {

            string query = "SELECT id FROM stores WHERE store_name=@str_id";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@str_id", selectedOption2);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _store_id = reader["id"].ToString();
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

        private void viewComboBox1()
        {
            string query = "SELECT product_id FROM products WHERE stock > 0";
            List<string> options = GetDataFromDatabase( query);

            // Menambahkan data ke ComboBox
            gunaComboBox1.Items.AddRange(options.ToArray());

            // Menambahkan ComboBox ke Form
            Controls.Add(gunaComboBox1);

            //MessageBox.Show(options.ToString());

            // Mengatur event handler untuk ComboBox
            gunaComboBox1.SelectedIndexChanged += gunaComboBox1_SelectedIndexChanged;
           

        }
        private void viewComboBox2()
        {


            string query = "SELECT store_name FROM stores";
            List<string> options = GetDataFromDatabase(query);

            // Menambahkan data ke ComboBox
            gunaComboBox2.Items.AddRange(options.ToArray());

            // Menambahkan ComboBox ke Form
            Controls.Add(gunaComboBox1);

            //MessageBox.Show(options.ToString());

            // Mengatur event handler untuk ComboBox
            gunaComboBox2.SelectedIndexChanged += gunaComboBox2_SelectedIndexChanged;
            
        }

        private void brg_out_Load(object sender, EventArgs e)
        {
            storeTXT.Visible = false;
            GenerateUniqueIDBarangKeluar();
            viewComboBox1();
            viewComboBox2();

        }

        private void gunaComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOption2= gunaComboBox2.SelectedItem.ToString();
            if(selectedOption2 != "")
            {
                gunaComboBox2.Visible = false;
                storeTXT.Visible = true;
                storeTXT.Text = selectedOption2;
            }
        }

        private void GenerateUniqueIDBarangKeluar()
        {
            DateTime currentTime = DateTime.Now;
            string timestamp = currentTime.ToString("yyyyMMddHHmmss");

            // Kode lokasi atau jenis barang
            string lokasiBarang = "OUT";

            this._IDKel = lokasiBarang + timestamp;
        }

        private void store()
        {
            string query = "INSERT INTO `out_transaction`(`out_transaction_id`, `user_id`, `store_id`,`create_at`, `description`) VALUES (@id_trans,@user_id,@store_id,@create,@des)";

            DateTime tanggal = DateTime.Now; // Tanggal saat ini

            string tanggalSql = tanggal.ToString("yyyy-MM-dd HH:mm:ss");

            string dek = "-";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_trans", _IDKel);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@store_id", _store_id);
                    command.Parameters.AddWithValue("@des", dek);
                    command.Parameters.AddWithValue("@create", tanggalSql);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(user_id.ToString());
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            detail_store();
            UpdateStock();
        }

        private void detail_store()
        {
            string query = "INSERT INTO `out_detail`(`id`, `out_transaction_id`, `product_id`, `qty`) VALUES (NULL,@id_trans,@product_id,@qty)";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_trans", _IDKel);
                    command.Parameters.AddWithValue("@product_id", selectedOption1);
                    command.Parameters.AddWithValue("@qty", qty.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Berhasil Checkout");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }
        private string getQty()
        {
            string query = "SELECT stock FROM products WHERE product_id = @product_id";
            string qty = "";
            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", selectedOption1);
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                qty = reader["stock"].ToString();
                            }
                        }
                        connection.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            return qty;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            getStore();
            store();
        }

        private void UpdateStock()
        {
            string getstock = getQty();
            string query = "UPDATE products SET stock =@stock WHERE product_id=@product_id";
            int qtyParse = int.Parse(getstock) - int.Parse(qty.Text);
            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", selectedOption1);
                    command.Parameters.AddWithValue("@stock", qtyParse.ToString());
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void clear()
        {
            selectedOption1 = "";
            selectedOption2 = "";
            qty.Text = "";
            gunaName.Text = "";
            gunaComboBox1.Text = "Select INC";
            //gunaComboBox2.Text = "Select STORE";

        }
    }
}
