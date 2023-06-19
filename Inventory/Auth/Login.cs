using Inventory.Admin;
using Inventory.layout;
using Inventory.config;
using Inventory.Auth;
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
        protected String _role = "";

        protected String _id;
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
        public Boolean setIsLoggedIn(Boolean i)
        {
            this.isLogged = i;
            return this.isLogged;
        }

        public String getRole()
        {
            return _role;
        }

        public String getID()
        {
            return _id;
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
                    _role = reader["role"].ToString();
                    _id = reader["id"].ToString();

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
                        if(_role == "0")
                        {
                            sidebar s = new sidebar(_id);
                            s.ShowDialog();
                            s.BringToFront();
                        }else if(_role == "1")
                        {
                            sidebarSu su = new sidebarSu(_id);
                            su.ShowDialog();
                            su.BringToFront();
                        }
                        else
                        {
                            verif verif = new verif();
                            verif.ShowDialog();
                        }
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

        private void Login_Load(object sender, EventArgs e)
        {
            this.Activate(); 
        }
    }
}
