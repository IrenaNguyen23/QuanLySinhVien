using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Connection
    {
        private static string stringConnection = @"Data Source=IRENE\SQLEXPRESS;Initial Catalog=NhanVien;Integrated Security=True";
        public static SqlConnection getConnection()
        {
            return new SqlConnection(stringConnection);
        }

    }

}
