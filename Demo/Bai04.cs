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
    public partial class Bai04 : Form
    {
        public Bai04()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string msg = null;
            int disc = 0;
            if (btnMale.Checked == true)

                msg += "Ông";

            if (btnFemale.Checked == true)

                msg += "Bà";

            if (ckDiscount.Checked == true)
                disc = 5;
            tbResult.Text = msg + " " + tbName.Text + " được giảm " + disc.ToString() + " % " + "\r\n";
        }

        private void ckDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDiscount.Checked == true)
            {
                tbDiscount.Enabled = true;
            }
            else
                tbDiscount.Enabled = false;
        }
    }
}
