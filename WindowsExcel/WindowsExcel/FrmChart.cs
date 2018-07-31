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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsExcel
{
    public partial class FrmChart : Form
    {

        private DataMange _mange = new DataMange();
        private List<ExcelModel> _list;
        private DataTable _table;
        public FrmChart(List<ExcelModel> list)
        {
            _list = list;
            InitializeComponent();
        }

        private void BindType()
        {
            var data = _mange.GetChartList();

            this.comboBoxType.DataSource = data;
            this.comboBoxType.DisplayMember = "Name";
            this.comboBoxType.ValueMember = "Id";
        }


        private void FrmChart_Load(object sender, EventArgs e)
        {
            BindType();
        }

        private void InitChart<T>(LineItems<T> itemData)
        {

            this.dataGridView1.DataSource = itemData.Table;

            var ser = this.chart1.Series[0];
            ser.Name = itemData.YLable;

            ser.Label = "#VAL";

            ser.ChartType = SeriesChartType.Line;
            ser.XValueType = ChartValueType.String;


            ser.Points.DataBindXY(itemData.XPoints, itemData.YPoints);

            ser.ToolTip = "#VALX:#VAL";

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshData()
        {
            var id = this.comboBoxType.SelectedValue.ToString();
            if (id == "0")
            {
                var data = _mange.GetErrItemLine(_list);
                InitChart(data);
                _table = data.Table;
            }
            if (id == "1")
            {
                var data = _mange.GetErrStateLine(_list);
                InitChart(data);
                _table = data.Table;
            }
            if (id == "2")
            {
                var data = _mange.GetErrMoneyLine(_list);
                InitChart(data);
                _table = data.Table;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (_table == null)
                return;
            SaveFileDialog fileDialog = new SaveFileDialog();

            //设置保存文件对话框的标题
            fileDialog.Title = "请选择要保存的文件路径";
            //初始化保存目录，默认exe文件目录
            fileDialog.InitialDirectory = Application.StartupPath;
            fileDialog.FileName = "excel_file.xlsx";
            //设置保存文件的类型
            fileDialog.Filter = "Excel.xlsx (*.xlsx)|*.xlsx";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //获得保存文件的路径
                    string filePath = fileDialog.FileName;
                    //保存
                    using (FileStream fsWrite = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        NPOIHelper.DataTableToExcel("sheet1", _table, ExcelExt.Xlsx, fsWrite);
                    }

                    MessageBox.Show("导出成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }

        }
    }
}

