using Inventory.config;
using Microsoft.VisualBasic.ApplicationServices;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory.Admin
{
    public partial class Brg_add : Form
    {
        private String _id_product,_name_product = "";
        private int _price,_qty = 0;
        protected String Mysql = "";
        protected string selectedOption = "";
        protected string _id_sup = "";
        public Brg_add()
        {
            InitializeComponent();
            Database db = new Database();
            Mysql = db.MysqlConn();
        }

        public Brg_add(String id_product, String name_product,int price)
        {
            _id_product = id_product;
            _name_product = name_product;
            _price = price;
            _qty = 0;
        }

        private void formClear()
        {
            INC_ID.Text = "";
            NAME_PRODUCT.Text = "";
            Harga.Text = "";
        }



        private void gunaButton1_Click(object sender, EventArgs e)
        {
            searchID();
            store();
        }

        private void store()
        {
            _id_product = INC_ID.Text;
            _name_product = NAME_PRODUCT.Text;
            _price = 0;
            _qty = 0;

            MySqlConnection cnn = new MySqlConnection(Mysql);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (id,product_id,supplier_id,product_name,stock,price) VALUES (NULL,@inc,@supp_id,@name,@qty,@harga)";
                cmd.Parameters.AddWithValue("@inc", INC_ID.Text);
                cmd.Parameters.AddWithValue("@name",NAME_PRODUCT.Text);
                cmd.Parameters.AddWithValue("@harga", Harga.Text);
                cmd.Parameters.AddWithValue("@qty", _qty);
                cmd.Parameters.AddWithValue("@supp_id", _id_sup);

                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Input data berhasil" + _name_product);
                formClear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

        private void Brg_add_Load(object sender, EventArgs e)
        {
            viewComboBox1();
        }

        private void supplier_id_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedOption = supplier_id.SelectedItem.ToString();
        }

       
        private void viewComboBox1()
        {
            string query = "SELECT supplier_name FROM suppliers";
            List<string> options = GetDataFromDatabase(query);

            // Menambahkan data ke ComboBox
            supplier_id.Items.AddRange(options.ToArray());

            // Menambahkan ComboBox ke Form
            Controls.Add(supplier_id);

            //MessageBox.Show(options.ToString());

            // Mengatur event handler untuk ComboBox
            supplier_id.SelectedIndexChanged += supplier_id_SelectedIndexChanged;

        }

        private void searchID()
        {

            string query = "SELECT id FROM suppliers WHERE supplier_name=@supp";

            using (MySqlConnection connection = new MySqlConnection(Mysql))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@supp", selectedOption);

                    try
                    {
                        connection.Open();
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _id_sup = reader["id"].ToString();
                        }
                        MessageBox.Show(_id_sup);

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
    }
}
