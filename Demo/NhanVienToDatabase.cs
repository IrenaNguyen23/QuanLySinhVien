using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class NhanVienToDatabase : Form
    {
        public NhanVienToDatabase()
        {
            InitializeComponent();
        }
        int index;
        Modify modify;
        NhanVien nv;
        private void NhanVienToDatabase_Load(object sender, EventArgs e)
        {
            modify = new Modify();
            try
            {
                dataGridView1.DataSource = modify.getAllNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = this.txtId.Text;
            string name = this.txtName.Text;
            string sex = (ckMale.Checked ? ckMale.Text : ckFemale.Text);
            DateTime dateOfBirth = this.dateTimePicker.Value;
            string address = this.txtAddress.Text;
            string phone = this.txtPhone.Text;
            nv = new NhanVien(id,name,sex,dateOfBirth,address,phone);
            if (checkData())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!");
                        return;
                    }
                }
                if (modify.insert(nv))
                {
                    dataGridView1.DataSource = modify.getAllNhanVien();
                }
                else
                {
                    MessageBox.Show("Lỗi:" + "Nhân viên đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }        
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = this.txtId.Text;
            string name = this.txtName.Text;
            string sex = (ckMale.Checked ? ckMale.Text : ckFemale.Text);
            DateTime dateOfBirth = this.dateTimePicker.Value;
            string address = this.txtAddress.Text;
            string phone = this.txtPhone.Text;
            nv = new NhanVien(id, name, sex, dateOfBirth, address, phone);
            if (checkData())
            if (modify.update(nv))
            {
                dataGridView1.DataSource = modify.getAllNhanVien();
            }
            else
            {
                MessageBox.Show("Lỗi:" + "Không sửa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if(modify.delete(id)) 
            {
                dataGridView1.DataSource = modify.getAllNhanVien();
            }
            else
            {
                MessageBox.Show("Lỗi:" + "Không xóa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public bool checkData()
        {
            if (string.IsNullOrEmpty(this.txtId.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtAddress.Text))
            {
                MessageBox.Show("Bạn chưa nhập  địa chỉ nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txtPhone.Text))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return false;
            }
            return true;
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            string text = txtId.Text;
            if (text.Any(c => !char.IsDigit(c)))
            {
                txtId.Text = string.Concat(text.Where(char.IsDigit)); // Chỉ giữ lại các ký tự số trong TextBox
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string text = txtName.Text;
            if (text.Any(char.IsDigit))
            {
                txtName.Text = string.Concat(text.Where(c => !char.IsDigit(c))); // Xóa các ký tự số khỏi TextBox
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string text = txtPhone.Text;
            if (text.Any(c => !char.IsDigit(c)))
            {
                txtPhone.Text = string.Concat(text.Where(char.IsDigit)); // Chỉ giữ lại các ký tự số trong TextBox
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            index = dataGridView1.CurrentCell == null ? -1 : dataGridView1.CurrentCell.RowIndex;
            if (index != -1)
            {
                txtId.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                txtName.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                string genderString = dataGridView1.Rows[index].Cells[2].Value.ToString();

                // Kiểm tra giá trị giới tính và đặt trạng thái Checked của RadioButton
                if (genderString == "Nam")
                {
                    ckMale.Checked = true;
                }
                else
                {
                    ckFemale.Checked = true;
                }
             
                // Lấy giá trị từ ô dữ liệu trong DataGridView
                string dateString = dataGridView1.Rows[index].Cells[3].Value.ToString();

                // Chuyển đổi chuỗi thành đối tượng DateTime
                DateTime dateTimeValue;
                if (DateTime.TryParse(dateString, out dateTimeValue))
                {
                    // Đặt giá trị cho DateTimePicker
                    dateTimePicker.Value = dateTimeValue;
                }
                txtAddress.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
                txtPhone.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel Sheet(*.xlsx) | *.xlsx|All Files(*.*)|*.*";
            if(op.ShowDialog() == DialogResult.OK)
            {
                string filepath = op.FileName;
                string con = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties ='Excel 8.0;HDR={1}' ";
                con = string.Format(con, filepath,"yes");
                OleDbConnection conn = new OleDbConnection(con);
                conn.Open();
                DataTable dtexcel = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string excelsheet = dtexcel.Rows[0]["TABLE_NAME"].ToString();
                OleDbCommand cmd = new OleDbCommand("Select * from [" + excelsheet + "]", conn);
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                oda.Fill(dt);
                conn.Close();
                dataGridView1.DataSource  = dt;
            }
        }
    }
}
