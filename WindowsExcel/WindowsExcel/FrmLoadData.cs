using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsExcel
{
    public partial class FrmLoadData : Form
    {
        public FrmLoadData()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel.xls (*.xls)|*.xls|Excel.xlsx (*.xlsx)|*.xlsx";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = openFile.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSheet.Text))
            {
                this.txtSheet.Focus();
                MessageBox.Show("Sheet页名不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(this.txtFilePath.Text))
            {
                this.txtFilePath.Focus();
                MessageBox.Show("文件路径不能为空！");
                return;
            }

            this.DialogResult = DialogResult.OK;

        }

        public ExcelFileData GetData()
        {
            ExcelFileData data = new ExcelFileData();
            data.FilePath = this.txtFilePath.Text.Trim();
            data.SheetName = this.txtSheet.Text.Trim();

            return data;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
