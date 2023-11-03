using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class UploadImageToDatabase : Form
    {
        public UploadImageToDatabase()
        {
            InitializeComponent();
        }

        private void UploadImageToDatabase_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void Insert(string fileName, byte[] Image)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Demo.Properties.Settings.DatabaseConnectionString"].ConnectionString))
            {
                if(cn.State == ConnectionState.Closed)
                    cn.Open();
                using(SqlCommand cmd = new SqlCommand("insert into pictures(filename, image) values(@filename,@image)",cn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@filename", txtFilename.Text);
                    cmd.Parameters.AddWithValue("@image", Image);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void LoadData()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Demo.Properties.Settings.DatabaseConnectionString"].ConnectionString))
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                using(DataTable dt = new DataTable("Pictures")) 
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("select * from pictures", cn);
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        byte[] ConvertImageToByte(Image image)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Image ConvertByArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image file(*.jpg;*.jpeg)|*.jpg;*.jpeg", Multiselect = false })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = Image.FromFile(ofd.FileName);
                    txtFilename.Text = ofd.FileName;
                    Insert(txtFilename.Text,ConvertImageToByte(pictureBox.Image));
                    LoadData();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            if (dt != null) 
            {
                DataRow row = dt.Rows[e.RowIndex];
                pictureBox.Image = ConvertByArrayToImage((byte[])row["Image"]); 
            }
        }
    }
}
