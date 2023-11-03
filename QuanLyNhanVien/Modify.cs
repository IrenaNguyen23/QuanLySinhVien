using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace QuanLyNhanVien
{
    class Modify
    {
        SqlDataAdapter adapter;
        SqlCommand cmd;
        public Modify() { }
        public DataTable getAllNhanVien()
        {
            DataTable dt = new DataTable();
            string query = "select * from tNhanVien";
            using (SqlConnection sqlConnection = Connection.getConnection())
            {
                sqlConnection.Open();
                adapter = new SqlDataAdapter(query, sqlConnection);
                adapter.Fill(dt);
                sqlConnection.Close();
            }
            return dt;
        }
        public bool insert(Employee nv)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "insert into tNhanVien values (@MaNV, @TenNV, @GioiTinh, @NgaySinh, @DiaChi, @SoDienThoai, @Image)";
            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nv.Id;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = nv.Name;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = nv.Sex;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.DateOfBirth.ToShortDateString();
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = nv.Address;
                cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar).Value = nv.Phonenumbe;
                cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = nv.Image;

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
        public bool update(Employee nv)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "update tNhanVien set TenNV = @TenNV, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Image = @Image where MaNV = @MaNV";

            try
            {
                sqlConnection.Open();
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nv.Id;
                cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar).Value = nv.Name;
                cmd.Parameters.Add("@GioiTinh", SqlDbType.VarChar).Value = nv.Sex;
                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = nv.DateOfBirth.ToShortDateString();
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = nv.Address;
                cmd.Parameters.Add("@SoDienThoai", SqlDbType.VarChar).Value = nv.Phonenumbe;
                cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = nv.Image;

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
        public bool delete(int id)
        {
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "delete from tNhanVien where MaNV = @MaNV";
            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@MaNV", id);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Row deleted from database successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting row from database: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }
    }
}
