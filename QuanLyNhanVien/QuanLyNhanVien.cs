using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using ExcelDataReader;

namespace QuanLyNhanVien
{
    public partial class QuanLyNhanVien : Form
    {
        public QuanLyNhanVien()
        {
            InitializeComponent();
        }
        Modify modify;
        Employee nv;
        int index;

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            string name = this.txtName.Text;
            string sex = (ckMale.Checked ? ckMale.Text : ckFemale.Text);
            DateTime dateOfBirth = this.dateTimePicker.Value;
            string address = this.txtAddress.Text;
            string phone = this.txtPhone.Text;
            string image = this.txtFilename.Text;
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return ;
            }
            int id ;
            
            if (this.txtId.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Mã nhân viên không được chứa ký tự chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return ;
            }
            id = int.Parse(this.txtId.Text);
            nv = new Employee(id, name, sex, dateOfBirth, address, phone, image);
            if (checkData())
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == id)
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
                    MessageBox.Show("Lỗi:" + "Không thêm được nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void QuanLyNhanVien_Load(object sender, EventArgs e)
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
                txtFilename.Text = dataGridView1.Rows[index].Cells[6].Value.ToString();
                string imagePath = txtFilename.Text;
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                pictureBox.Image = image;

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id;
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Cells[0].Value != null)
            {
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out id))
                {
                    if (modify.delete(id))
                    {
                        dataGridView1.DataSource = modify.getAllNhanVien();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Không xóa được", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi: Giá trị không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lỗi: Không có hàng được chọn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public class ExcelExporter
        {
            public ExcelExporter()
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            }

            public void ExportToExcel(DataGridView dataGridView)
            {
                // Kiểm tra nếu DataGridView không có dữ liệu
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất ra Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    // Tạo một package Excel
                    using (ExcelPackage package = new ExcelPackage())
                    {

                        // Tạo một worksheet mới
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        // Ghi dòng tiêu đề
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            worksheet.Cells[1, j + 1].Value = dataGridView.Columns[j].HeaderText;
                        }

                        // Ghi dữ liệu từ DataGridView vào worksheet
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                if (dataGridView.Columns[j].Name == "NgaySinh")
                                {
                                    DateTime ngaySinh = Convert.ToDateTime(dataGridView.Rows[i].Cells[j].Value);
                                    worksheet.Cells[i + 2, j + 1].Value = ngaySinh.ToString("dd/MM/yyyy");
                                }
                                else
                                {
                                    worksheet.Cells[i + 2, j + 1].Value = dataGridView.Rows[i].Cells[j].Value;
                                }
                            }
                        }

                        // Lưu file Excel
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";
                        saveFileDialog.FileName = "Data.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileInfo fi = new FileInfo(saveFileDialog.FileName);
                            package.SaveAs(fi);
                            MessageBox.Show("Xuất dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xuất dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExcelExporter exporter = new ExcelExporter();
            exporter.ExportToExcel(dataGridView1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
            string name = this.txtName.Text;
            string sex = (ckMale.Checked ? ckMale.Text : ckFemale.Text);
            DateTime dateOfBirth = this.dateTimePicker.Value;
            string address = this.txtAddress.Text;
            string phone = this.txtPhone.Text;
            string image = this.txtFilename.Text;
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return;
            }
            int id;

            if (this.txtId.Text.Any(char.IsLetter))
            {
                MessageBox.Show("Mã nhân viên không được chứa ký tự chữ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtId.Focus();
                return;
            }
            id = int.Parse(this.txtId.Text);
            nv = new Employee(id, name, sex, dateOfBirth, address, phone, image);
            
             if (modify.update(nv))
             {
                 dataGridView1.DataSource = modify.getAllNhanVien();
             }
             else
             {
                 MessageBox.Show("Lỗi:" + "Không sửa được nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
            
        }
        public class ExcelImporter
        {
            public DataTable ImportFromExcel()
            {
                DataTable dataTable = new DataTable();

                // Chọn file Excel để import
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

                        // Đọc dữ liệu từ worksheet và thêm vào DataTable
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        // Đọc dòng tiêu đề
                        for (int j = 1; j <= colCount; j++)
                        {
                            dataTable.Columns.Add(worksheet.Cells[1, j].Value.ToString());
                        }

                        // Đọc dữ liệu từ các dòng còn lại
                        for (int i = 2; i <= rowCount; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int j = 1; j <= colCount; j++)
                            {
                                dataRow[j - 1] = worksheet.Cells[i, j].Value;
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }

                return dataTable;
            }
        }
        public class DatabaseManager
        {
            private string connectionString = @"Data Source=IRENE\SQLEXPRESS;Initial Catalog=QLNV;Integrated Security=True";

            public void AddDataToDatabase(DataTable dataTable)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                        {
                            bulkCopy.DestinationTableName = "tNhanVIen";
                            bulkCopy.WriteToServer(dataTable);
                        }

                        MessageBox.Show("Data added to database successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding data to database: " + ex.Message);
                    }
                }
            }
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            ExcelImporter importer = new ExcelImporter();
            DataTable dataTable = importer.ImportFromExcel();

            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.AddDataToDatabase(dataTable);
        }
    }
}
