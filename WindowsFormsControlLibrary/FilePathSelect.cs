using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary1
{
    public partial class FilePathSelect : UserControl
    {
        public FilePathSelect()
        {
            InitializeComponent();
        }

        public string FileName
        {
            get
            {
                return txtPath.Text;
            }
            set
            {
                txtPath.Text = value;
            }
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = txtPath.Text;
            if (ofd.ShowDialog() == DialogResult.OK)
                txtPath.Text = ofd.FileName;
        }
    }
}
