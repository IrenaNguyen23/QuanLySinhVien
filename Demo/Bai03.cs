using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Bai03 : Form
    {
        public Bai03()
        {
            InitializeComponent();
        }

        private void Bai03_Load(object sender, EventArgs e)
        {
            ArrayList lst = GetData();
            comboBox1.DataSource = lst;
            comboBox1.DisplayMember = "Name";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.ValueMember = "Id";
            String id = comboBox1.SelectedValue.ToString();
            tbDisplay.Text = "Bạn đã chọn khoa có mã: " + id;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            comboBox1.ValueMember = "Name";
            String name = comboBox1.SelectedValue.ToString();
            tbDisplay.Text = "Bạn là sinh viên khoa: " + name;
        }
        public ArrayList GetData()
        {
            ArrayList lst = new ArrayList();

            Faculty f = new Faculty();
            f.Id = "K01";
            f.Name = "Công nghệ thông tin";
            f.Quantity = 1200;
            lst.Add(f);

            f = new Faculty();
            f.Id = "K02";
            f.Name = "Quản trị kinh doanh";
            f.Quantity = 4200;
            lst.Add(f);

            f = new Faculty();
            f.Id = "K03";
            f.Name = "kế toán tài chính";
            f.Quantity = 5200;
            lst.Add(f);

            return lst;
        }
    }
}
