﻿using Inventory.Admin;
using Inventory.Admin.store;
using Inventory.Admin.suppliers;
using Inventory.config;
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

namespace Inventory
{
    public partial class Store : Form
    {
        protected String _db_conn = "";
        protected string selectedID = "";
        public Store()
        {
            InitializeComponent();
            Database db = new Database();
            _db_conn = db.MysqlConn();
        }

        private void Stockbarang_Load(object sender, EventArgs e)
        {
            viewData();
            //btnStock.BackColor= Color.Blue;
            
        }

        // Buat koneksi ke database

        private void viewData()
        {
            MySqlConnection cnn = new MySqlConnection(_db_conn);
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            string sql = "SELECT * FROM stores";
            // Buat perintah SQL untuk mengambil seluruh data dari tabel
            MySqlCommand command = new MySqlCommand(sql, cnn);

            // Buat objek DataAdapter dan DataTable
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            // Isi data dari database ke dalam DataTable
            adapter.Fill(dataTable);

            // Tampilkan data dari DataTable ke dalam DataGridView
            gunaDataGridView1.DataSource = dataTable;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
           
         }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
                selectedID = gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
               
        }

        private void btn_hps_Click(object sender, EventArgs e)
        {
            if (selectedID == "")
            {
                MessageBox.Show("Pilih Store Terlebih Dahulu!");
            }
            else
            {
                // Query SQL untuk mengambil data dari tabel
                string query = "DELETE  FROM stores WHERE id=@_id";

                using (MySqlConnection connection = new MySqlConnection(_db_conn))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@_id", selectedID);
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("DATA BERHASIL DI HAPUS");
                            connection.Close();
                    
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                viewData();
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            if (selectedID == "")
            {
                MessageBox.Show("Pilih Store Terlebih Dahulu!");
            }
            else
            {
                Store_edit str_ed = new Store_edit(selectedID);
                str_ed.ShowDialog();
                viewData();
            }
        }

        private void gunaButton1_Click_1(object sender, EventArgs e)
        {
          
            Store_add str_ad = new Store_add();
            str_ad.ShowDialog();
            viewData();
        }

        public string getID()
        {
            return selectedID;
        }
    }
}
