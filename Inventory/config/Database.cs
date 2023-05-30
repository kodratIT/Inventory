using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.config
{
    internal class Database
    {
        private string DB = "server=localhost;database=toko1;uid=root;pwd=\"\"";

        public string MysqlConn()
        {
            return DB;
        }
    }
}
