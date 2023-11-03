using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Modify
    {
        SqlDataAdapter adapter;
        SqlCommand cmd;
        public Modify() { }
        public DataTable getAllNhanVien()
        {
            DataTable dt = new DataTable();
            string query = "select * from NhanVien";
            using (SqlConnection sqlConnection = Connection.getConnection())
            {
                sqlConnection.Open();
                adapter = new SqlDataAdapter(query, sqlConnection);
                adapter.Fill(dt);
                sqlConnection.Close();
            }
            return dt;
        }
        public bool insert(NhanVien nv)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "insert into NhanVien values (@MaNV, @TenNV, @GioiTinh, @NgaySinh, @DiaChi, @SoDienThoai)";
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add("@MaNV", SqlDbType.NChar).Value = nv.Id;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = nv.Name;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = nv.Sex;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.DateOfBirth.ToShortDateString();
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = nv.Address;
                cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar).Value = nv.Phonenumbe;

                cmd.ExecuteNonQuery();
            }
            catch 
            {
                return false;
            }
            finally 
            {
                sqlConnection.Close();
            }
            return true;
        }
        public bool update(NhanVien nv)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "update NhanVien set TenNV=@TenNV,GioiTinh=@GioiTinh,NgaySinh=@NgaySinh,DiaChi=@DiaChi,SoDienThoai=@SoDienThoai where MaNV=@MaNV";
        
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add("@MaNV", SqlDbType.NChar).Value = nv.Id;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = nv.Name;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.VarChar).Value = nv.Sex;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.DateOfBirth.ToShortDateString();
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = nv.Address;
                cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar).Value = nv.Phonenumbe;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }
        public bool delete(string id)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "delete NhanVien where MaNV = @MaNV";
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add("@MaNV", SqlDbType.NChar).Value = id;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }
    }
}
