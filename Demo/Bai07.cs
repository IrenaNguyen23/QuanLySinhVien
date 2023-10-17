using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Demo
{
    public partial class Bai07 : Form
    {
        List<Employee> lst;
        int index;
        public Bai07()
        {
            InitializeComponent();
        }
        public List<Employee> GetData()
        {
           List<Employee> list = new List<Employee>();
           Employee emp = new Employee();
            emp.Id = "54321";
            emp.Name = "Nguyên Duy Hòa";
            emp.Age = 20;
            list.Add(emp);

            emp = new Employee();
            emp.Id = "54322";
            emp.Name = "Trần Tiến Thuận";
            emp.Age = 20;
            list.Add(emp);

            return list;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            if (checkData())
            {
                foreach (DataGridViewRow row in dgvNhanVien.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == id)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!");
                        return;
                    }
                }
                dgvNhanVien.Rows.Add(txtId.Text, txtName.Text, txtAge.Text, ckGender.Checked, pbImage.Image);
                dgvNhanVien.RefreshEdit();
            }

        }

        private void Bai07_Load(object sender, EventArgs e)
        {
            lst = GetData();
            foreach(Employee em in lst)
            {
                dgvNhanVien.Rows.Add(em.Id,em.Name,em.Age);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa nhân viên này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dgvNhanVien.Rows.RemoveAt(index);
                dgvNhanVien.RefreshEdit();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string name = txtName.Text;
            if (checkData())
            {
                if (dgvNhanVien.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                    selectedRow.Cells[0].Value = txtId.Text;
                    selectedRow.Cells[1].Value = txtName.Text;
                    selectedRow.Cells[2].Value = txtAge.Text;
                    selectedRow.Cells[3].Value = ckGender.Checked;
                    selectedRow.Cells[4].Value = pbImage.Image;
                    dgvNhanVien.RefreshEdit();
                }   
            }
        }
        public bool checkData()
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Bạn chưa nhập tuổi nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAge.Focus();
                return false;
            }
            return true;
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            index = dgvNhanVien.CurrentCell == null ? -1 : dgvNhanVien.CurrentCell.RowIndex;
            if (index != -1)
            {
                txtId.Text = dgvNhanVien.Rows[index].Cells[0].Value.ToString();
                txtName.Text = dgvNhanVien.Rows[index].Cells[1].Value.ToString();
                txtAge.Text = dgvNhanVien.Rows[index].Cells[2].Value.ToString();
                pbImage.Image = (Image)dgvNhanVien.Rows[index].Cells[4].Value;
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

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            string text = txtAge.Text;
            if (text.Any(c => !char.IsDigit(c)))
            {
                txtAge.Text = string.Concat(text.Where(char.IsDigit)); // Chỉ giữ lại các ký tự số trong TextBox
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "JPEG files (*.jpg) |*.jpg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = dlg.FileName;
            }
        }
    }
}
