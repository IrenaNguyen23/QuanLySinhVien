using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Demo
{
    public partial class QuanLyNhanVien : Form
    {
        string flag;
        DataTable dtNV;
        int index;
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }
        public DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNV");
            dt.Columns.Add("TenNV");
            dt.Columns.Add("Tuoi");
            dt.Columns.Add("GioiTinh");
            return dt;
        }
        public void LockControl()
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            btnSave.Enabled = false;
            btnHuy.Enabled = false;

            txtMaNv.ReadOnly = true;
            txtTenNv.ReadOnly = true;
            txtTuoi.ReadOnly = true;

            btnAdd.Focus();
        }
        public void UnlockControl() 
        { 
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnHuy.Enabled = true;

            txtMaNv.ReadOnly = false;
            txtTenNv.ReadOnly = false;
            txtTuoi.ReadOnly = false;

            txtMaNv.Focus();
            
        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LockControl();
            dtNV = createTable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "add";

            txtMaNv.Text = "";
            txtTenNv.Text = "";
            txtTuoi.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UnlockControl();
            flag = "edit";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string id = txtMaNv.Text;
            string name = txtTenNv.Text;

            if (flag == "add") {
                if (checkData())
                {
                    foreach(DataGridViewRow row in dgvNhanVien.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                        {
                            MessageBox.Show("Mã nhân viên đã tồn tại!");
                            return;
                        }
                    }
                    dtNV.Rows.Add(txtMaNv.Text,txtTenNv.Text,txtTuoi.Text, ckGioiTinh.Checked);
                    dgvNhanVien.DataSource = dtNV;
                    dgvNhanVien.RefreshEdit();
                }
                
            }
            else if(flag == "edit")
            {
                if (checkData())
                {
                    dtNV.Rows[index][0] = txtMaNv.Text;
                    dtNV.Rows[index][1] = txtTenNv.Text;
                    dtNV.Rows[index][2] = txtTuoi.Text;
                    dtNV.Rows[index][3] = ckGioiTinh.Checked;
                    dgvNhanVien.DataSource = dtNV;
                    dgvNhanVien.RefreshEdit();
                }
            }
            LockControl();
        }
        public bool checkData()
        {
            if(string.IsNullOrEmpty(txtMaNv.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNv.Focus() ;
                return false;
            }
            if (string.IsNullOrEmpty(txtTenNv.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNv.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTuoi.Text))
            {
                MessageBox.Show("Bạn chưa nhập tuổi nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTuoi.Focus();
                return false;
            }
            return true;
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            index = dgvNhanVien.CurrentCell == null ? -1 : dgvNhanVien.CurrentCell.RowIndex;
            if(index != -1 )
            {
                txtMaNv.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
                txtTenNv.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
                txtTuoi.Text = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn xóa nhân viên này?","Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dtNV.Rows.RemoveAt(index);
                dgvNhanVien.DataSource = dtNV;
                dgvNhanVien.RefreshEdit();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTenNv_TextChanged(object sender, EventArgs e)
        {
            string text = txtTenNv.Text;
            if (text.Any(char.IsDigit))
            {
                txtTenNv.Text = string.Concat(text.Where(c => !char.IsDigit(c))); // Xóa các ký tự số khỏi TextBox
            }
        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            string text = txtTuoi.Text;
            if (text.Any(c => !char.IsDigit(c)))
            {
                txtTuoi.Text = string.Concat(text.Where(char.IsDigit)); // Chỉ giữ lại các ký tự số trong TextBox
            }
        }

    }
}
