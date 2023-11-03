using System;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btn01_Click(object sender, EventArgs e)
        {
            Bai01 bai01 = new Bai01();
            bai01.ShowDialog();
        }

        private void btn02_Click(object sender, EventArgs e)
        {
            Bai02 bai02 = new Bai02();
            bai02.ShowDialog();
        }

        private void btn03_Click(object sender, EventArgs e)
        {
            Bai03 bai03 = new Bai03();
            bai03.ShowDialog();
        }

        private void btn04_Click(object sender, EventArgs e)
        {
            Bai04 bai04 = new Bai04();
            bai04.ShowDialog();
        }

        private void btn05_Click(object sender, EventArgs e)
        {
            Bai05 bai05 = new Bai05();
            bai05.ShowDialog();
        }

        private void btn06_Click(object sender, EventArgs e)
        {
            QuanLyNhanVien bai06 = new QuanLyNhanVien();
            bai06.ShowDialog();
        }

        private void btn07_Click(object sender, EventArgs e)
        {
            Bai07 bai07 = new Bai07();
            bai07.ShowDialog();
        }

        private void btn08_Click(object sender, EventArgs e)
        {
            UploadImageToDatabase bai08 = new UploadImageToDatabase();
            bai08.ShowDialog();
        }

        private void btn09_Click(object sender, EventArgs e)
        {
            NhanVienToDatabase bai09 = new NhanVienToDatabase();
            bai09.ShowDialog();
        }
    }
}
