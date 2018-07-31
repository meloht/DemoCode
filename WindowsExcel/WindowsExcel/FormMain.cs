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
        private DataItem _data = null;

        
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
                Utils.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    var fdata = frmLoad.GetData();

                    var data = _mange.GetData(fdata.SheetName, fdata.FilePath);

                    if (!data.IsSuccess)
                    {
                        MessageBox.Show(data.ErrMessage);
                    }
                    else
                    {
                        _data = data.Data;
                        Action<DataTable> del = new Action<DataTable>(BindData);
                        this.BeginInvoke(del, data.Data.Table);
                        
                    }
                }, null);
              
            }
        }

        private void BindData(DataTable dt)
        {
            this.dataGridView1.DataSource = dt;
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           
           
        }

        private void toolStripBtnChart_Click(object sender, EventArgs e)
        {
            if (_data == null)
                return;
            FrmChart frm = new FrmChart(_data.List);
            frm.Show();
        }
    }
}
