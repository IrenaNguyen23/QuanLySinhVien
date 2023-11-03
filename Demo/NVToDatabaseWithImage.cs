using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Demo
{
    public partial class NVToDatabaseWithImage : Form
    {
        SqlDataAdapter adapter;
        public NVToDatabaseWithImage()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "JPEG files (*.jpg)| *.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox.ImageLocation = dlg.FileName;
                txtFilename.Text = dlg.FileName;
            }
        }

        private void NVToDatabaseWithImage_Load(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string stringConn = @"Data Source=IRENE\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True";
                SqlConnection conn = new SqlConnection(stringConn);
                conn.Open();
                string query = "insert into NhanVien values (@MaNV, @TenNV, @GioiTinh, @NgaySinh, @DiaChi, @SoDienThoai, @Image)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", txtId.Text);
                cmd.Parameters.AddWithValue("@TenNV", txtName.Text);
                string radioButtonValue = "";

                if (ckMale.Checked)
                {
                    radioButtonValue = "Nam";
                }
                else
                {
                    radioButtonValue = "Nữ";
                }
                cmd.Parameters.AddWithValue("@GioiTinh", radioButtonValue);
                cmd.Parameters.AddWithValue("@NgaySinh", dateTimePicker.Value);
                cmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Image", convertImageToByte());
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Success");
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] convertImageToByte()
        {
            FileStream fs;
            fs=new FileStream(txtFilename.Text, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];
            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();
            return picbyte;
        }
    }
}
