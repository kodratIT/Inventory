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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Inventory.Admin
{
    public partial class Brg_In : Form
    {

       // private ComboBox comboBox;
        protected String Mysql = "server=localhost;database=toko1;uid=root;pwd=\"\"";
        protected String selectedOption = "";
        protected string selectedSup ="";
        protected string _supplier_id = "";
        protected int user_id = 1;
        public Brg_In()
        {
            InitializeComponent();
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOption = comboBox1.SelectedItem.ToString();
            searchName();
            getSupplier(_supplier_id);
          
        }

       

        private void searchName()
        {

            string query = "SELECT product_name,supplier_id FROM products WHERE product_id=@product_id";
            
            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", selectedOption);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                           Name_Product.Text = reader["product_name"].ToString();
                           _supplier_id = reader["supplier_id"].ToString();
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

        private void getSupplier(string supplier)
        {

            string query = "SELECT supplier_name FROM suppliers WHERE id=@supp_id";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@supp_id", _supplier_id);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            supp_id_name.Text = reader["supplier_name"].ToString();
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
      
        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void brg_in_Load(object sender, EventArgs e)
        {
            viewComboBox1();



        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void viewComboBox1()
        {

            
            string query = "SELECT product_id FROM products";
            List<string> options = GetDataFromDatabase(query);

            // Menambahkan data ke ComboBox
            comboBox1.Items.AddRange(options.ToArray());

            // Menambahkan ComboBox ke Form
            Controls.Add(comboBox1);

            //MessageBox.Show(options.ToString());

            // Mengatur event handler untuk ComboBox
            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            if (selectedOption != "")
            {
                searchName();
                
            }
          
        }

       

        private void gunaButton4_Click(object sender, EventArgs e)
        {

            if (selectedOption != "")
            {
                store();
            }else 
            {
                MessageBox.Show("Lengkapi Form Terlebih Dahulu!");
            }
           

        }


        private void store()
        {
            string query = "INSERT INTO `in_transaction`(`id_transaction`, `user_id`, `create_at`, `description`) VALUES (@id_trans,@user_id,@create,@des)";

            DateTime tanggal = DateTime.Now; // Tanggal saat ini

            string tanggalSql = tanggal.ToString("yyyy-MM-dd HH:mm:ss");

            string dek = "-";
            string uniqueID = GenerateUniqueIDBarangMasuk();

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_trans", uniqueID);
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@des", dek);
                    command.Parameters.AddWithValue("@create",tanggalSql );

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

            detail_store(uniqueID);
            UpdateStock();
        }


        private void detail_store(string unique)
        {
            string query = "INSERT INTO `in_detail`(`id`, `transaction_in_id`, `product_id`, `qty`) VALUES (NULL,@id_trans,@product_id,@qty)";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_trans", unique);
                    command.Parameters.AddWithValue("@product_id",selectedOption );
                    command.Parameters.AddWithValue("@qty", qty.Text);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Data Berhasil di Input");

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
                    command.Parameters.AddWithValue("@product_id", selectedOption);
                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Ambil data dari setiap baris
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


        private void UpdateStock()
        {
            string getstock = getQty();
            string query = "UPDATE products SET stock =@stock WHERE product_id=@product_id";
            int qtyParse = int.Parse(getstock) + int.Parse(qty.Text);
            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", selectedOption);
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

        private string GenerateUniqueIDBarangMasuk()
        {
            string IDBarangMasuk = string.Empty;

            DateTime currentTime = DateTime.Now;
            string timestamp = currentTime.ToString("yyyyMMddHHmmss");

            // Kode lokasi atau jenis barang
            string lokasiBarang = "IN";

            IDBarangMasuk = lokasiBarang + timestamp;

            return IDBarangMasuk;
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            selectedOption = "";
            qty.Text = "";
            Name_Product.Text = "";
            comboBox1.Text = "Select INC";
        }

        
    }

}
