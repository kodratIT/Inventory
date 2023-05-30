using Inventory.config;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Register : Form
    {

        protected String _Email = "";
        protected String _User = "";
        protected String _Pass = "";
        protected String Mysql = "";

        public Register()
        {
            InitializeComponent();
            Database db = new Database();
            Mysql = db.MysqlConn();
        }


        private void checkUsername()
        {

            MySqlConnection cnn = new MySqlConnection(Mysql);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE username=@user";
                cmd.Parameters.AddWithValue("@user", txtUser.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _User = reader["username"].ToString();
                }
                reader.Close();
                cnn.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkEmail()
        {

            MySqlConnection cnn = new MySqlConnection(Mysql);
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                cnn.Open();
                cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE email=@email";
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _Email = reader["email"].ToString();
                }
                reader.Close();
                cnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Store()
        {
            checkUsername();
            checkEmail();
            if(txtName.Text == "" || txtEmail.Text == "" || txtPass.Text == "" || txtUser.Text == "")
            {
                MessageBox.Show("Silakan Lengkapi Data Terlebih dahulu!");
            }
            else
            {
                if(_User == "")
                {
                    if(_Email == "")
                    {
                        MySqlConnection cnn = new MySqlConnection(Mysql);
                        MySqlCommand cmd = new MySqlCommand();
                        try
                        {
                            cnn.Open();
                            cmd = cnn.CreateCommand();
                            cmd.CommandText = "INSERT INTO users (name,email,username,password) VALUES (@name,@email,@user,@pass)";
                            cmd.Parameters.AddWithValue("@name", txtName.Text);
                            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                            cmd.Parameters.AddWithValue("@user", txtUser.Text);
                            cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                            MessageBox.Show("Input data berhasil" + _User);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email Telah digunakan");
                    }
                }
                else
                {
                    MessageBox.Show("Username Telah digunakan");

                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btn_regis_Click(object sender, EventArgs e)
        {
            Store();
        }
    }
}
