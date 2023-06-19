using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Inventory.config
{
    internal class DataStorage
    {
        private static string dataFilePath = "data.txt";

        public static void SaveDataToFile()
        {
            // Mengambil data yang ingin disimpan (misalnya dari variabel global, objek, atau sumber data lainnya)
            string data = "Contoh data yang akan disimpan";

            // Menyimpan data ke berkas
            File.WriteAllText(dataFilePath, data);
        }

        public static void LoadDataFromFile()
        {
            if (File.Exists(dataFilePath))
            {
                // Memuat data dari berkas
                string data = File.ReadAllText(dataFilePath);

                // Menggunakan data yang telah dimuat (misalnya memasukkan ke variabel global atau objek)
                Console.WriteLine("Data yang dimuat: " + data);
            }
        }
    }
}
