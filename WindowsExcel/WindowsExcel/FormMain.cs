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
    public partial class FormMain : Form
    {
        private DataMange _mange = new DataMange();
        public FormMain()
        {
            InitializeComponent();
        }

        private void toolStripbtnLoad_Click(object sender, EventArgs e)
        {
            FrmLoadData frmLoad = new FrmLoadData();
            var dr= frmLoad.ShowDialog();
            if (dr == DialogResult.OK)
            {
                var fdata = frmLoad.GetData();

                var data = _mange.GetData(fdata.SheetName, fdata.FilePath);

                if (!data.IsSuccess)
                {
                    MessageBox.Show(data.ErrMessage);
                }
                else
                {
                    this.dataGridView1.DataSource = data.Data.Table;
                }
            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
