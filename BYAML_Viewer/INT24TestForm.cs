using BYAML_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYAML_Viewer
{
    public partial class INT24TestForm : Form
    {
        public INT24TestForm()
        {
            InitializeComponent();
        }

        private void INT24TestForm_Load(object sender, EventArgs e)
        {
            CustomValueTypeClass.Int24 int24 = -16;
            textBox1.Text = int24.ToString();

            CustomValueTypeClass.Int24 Converted = CustomValueTypeClass.ToInt24(new byte[] { 0x18, 0x00, 0x00 }, 0);
            textBox2.Text = Converted.ToString();

            CustomValueTypeClass.Int24 i = 3;
            CustomValueTypeClass.Int24 Int24Data = 60;
            if (i < Int24Data)
            {
                MessageBox.Show("Test");
            }

            var d = CustomValueTypeClass.GetBytes(345728);
            textBox3.Text = d[0].ToString();
            textBox4.Text = d[1].ToString();
            textBox5.Text = d[2].ToString();
        }
    }
}
