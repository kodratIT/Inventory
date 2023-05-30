using Inventory.Admin;
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

namespace Inventory
{
    public partial class Login : Form
    {
        protected String _username = "";
        protected String _password = "";
        protected Boolean isLogged = false;
        protected String _db_conn = "";
        public Login()
        {
            InitializeComponent();
            Database db= new Database();
            _db_conn = db.MysqlConn();
        }

        public Boolean IsLoggedIn()
        {
            return isLogged;
        }

        private Boolean validasi(String user)
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE username=@user";
                cmd.Parameters.AddWithValue("@user", user);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _username = reader["username"].ToString();
                    _password = reader["password"].ToString();
                }
                reader.Close();
                cnn.Close();

                if (_username != "" && _password != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            isLogged = validasi(txtUser.Text);
            if (txtUser.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Masukan Username & Password");
            }
            else
            {
                if (isLogged == true)
                {
                    if (_password == txtPass.Text)
                    {
                        this.Hide();
                        sidebar s = new sidebar();
                        s.ShowDialog();
                        s.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("Username & Password Salah!");
                    }
                }
                else
                {
                    MessageBox.Show("Username & Password Salah!");
                }
            }
        }
    }
}
