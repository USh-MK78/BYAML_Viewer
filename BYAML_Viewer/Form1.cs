using BYAML_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYAML_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openBYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Open_BYAML = new OpenFileDialog
            {
                Title = "Open BYAML",
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "byaml file|*.byaml"
            };

            if (Open_BYAML.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(Open_BYAML.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);

                BYAML bYAML = new BYAML();
                bYAML.ReadBYAML(br, EndianConvert.GetEnumEndianToBytes(EndianConvert.Endian.BigEndian));

                br.Close();
                fs.Close();

                propertyGrid1.SelectedObject = bYAML;
            }
        }

        private void saveBYAMLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iNT24ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            INT24TestForm iNT24TestForm = new INT24TestForm();
            iNT24TestForm.Show();

        }
    }
}
