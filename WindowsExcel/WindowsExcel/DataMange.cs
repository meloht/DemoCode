using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public class DataMange
    {
        public ResultData<DataItem> GetData(string sheetName, string path)
        {
            ResultData<DataItem> result = new ResultData<DataItem>();
            result.IsSuccess = true;
            try
            {
                result.Data = new DataItem();
                result.Data.List = new List<ExcelModel>();

                DataTable dt = NPOIHelper.ExcelToDataTable(sheetName, true, path);

                result.Data.Table = dt;

                if (dt == null || dt.Rows.Count == 0)
                    return result;


                foreach (DataRow item in dt.Rows)
                {
                    ExcelModel model = new ExcelModel();

                    model.ExDate = Utils.ToDateTime(item["日期"].ToString());
                    model.ExErrMoney = Utils.ToDouble(item["报废金额"].ToString());
                    model.ExErrNum = Utils.ToInt(item["报废数量"].ToString());
                    model.ExErrState = item["报废状态描述"].ToString();
                    model.ExItemName = item["零件名称"].ToString();

                    model.ExItemNo = item["零件号"].ToString();
                    model.ExNo = item["单号"].ToString();
                    model.ExPeriod = item["周期跟物流核对"].ToString();
                    model.ExRoom = item["车间"].ToString();
                    model.ExPrice = Utils.ToDouble(item["单价"].ToString());

                    model.ExProject = item["项目"].ToString();
                    model.ExTypeCode = item["类别代码"].ToString();
                    model.ExUnit = item["单位"].ToString();

                    result.Data.List.Add(model);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrMessage = ex.Message;
            }

            return result;
        }

        public List<ItemModel> GetChartList()
        {
            List<ItemModel> list = new List<ItemModel>();

            list.Add(new ItemModel() { Id = 0, Name = "零件号" });
            list.Add(new ItemModel() { Id = 1, Name = "报废状态" });
            list.Add(new ItemModel() { Id = 2, Name = "报废金额" });
            return list;
        }



        public LineItems<double> GetErrMoneyLine(List<ExcelModel> list)
        {
            LineItems<double> item = new LineItems<double>();

            var xdata = list.Where(p => string.IsNullOrEmpty(p.ExPeriod) == false).Select(p => p.ExPeriod).Distinct().ToList();
            List<double> ylist = new List<double>();

            DataTable dt = new DataTable();
            dt.Columns.Add("周期跟物流核对");
            dt.Columns.Add("报废金额");

            item.XLable = "周期跟物流核对";
            item.YLable = "报废金额";

            foreach (var row in xdata)
            {
                var num = list.Where(p => p.ExPeriod == row && p.ExErrMoney != null).Sum(p => p.ExErrMoney);
                double numd = 0;
                if (num != null)
                {
                    numd = Math.Round(num.Value, 2);
                }

                ylist.Add(numd);

                var rowd = dt.NewRow();
                rowd["报废金额"] = numd;
                rowd["周期跟物流核对"] = row;

                dt.Rows.Add(rowd);
            }


            item.XPoints = xdata;
            item.YPoints = ylist;
            item.Table = dt;
            return item;
        }


        public LineItems<int> GetErrStateLine(List<ExcelModel> list)
        {
            LineItems<int> item = new LineItems<int>();

            var xdata = list.Where(p => string.IsNullOrEmpty(p.ExPeriod) == false).Select(p => p.ExPeriod).Distinct().ToList();
            List<int> ylist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("周期跟物流核对");
            dt.Columns.Add("报废状态数量");

            item.XLable = "周期跟物流核对";
            item.YLable = "报废状态数量";

            foreach (var row in xdata)
            {
                var num = list.Where(p => p.ExPeriod == row
                && string.IsNullOrEmpty(p.ExErrState) == false).Select(p => p.ExErrState).Distinct().Count();


                ylist.Add(num);

                var rowd = dt.NewRow();
                rowd["报废状态数量"] = num;
                rowd["周期跟物流核对"] = row;

                dt.Rows.Add(rowd);
            }


            item.XPoints = xdata;
            item.YPoints = ylist;
            item.Table = dt;
            return item;
        }

        public LineItems<int> GetErrItemLine(List<ExcelModel> list)
        {
            LineItems<int> item = new LineItems<int>();

            var xdata = list.Where(p => string.IsNullOrEmpty(p.ExPeriod) == false).Select(p => p.ExPeriod).Distinct().ToList();
            List<int> ylist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("周期跟物流核对");
            dt.Columns.Add("零件数量");

            item.XLable = "周期跟物流核对";
            item.YLable = "零件数量";

            foreach (var row in xdata)
            {
                var num = list.Where(p => p.ExPeriod == row
                && string.IsNullOrEmpty(p.ExItemNo) == false).Select(p => p.ExItemNo).Distinct().Count();


                ylist.Add(num);

                var rowd = dt.NewRow();
                rowd["零件数量"] = num;
                rowd["周期跟物流核对"] = row;

                dt.Rows.Add(rowd);
            }


            item.XPoints = xdata;
            item.YPoints = ylist;
            item.Table = dt;
            return item;
        }


     


    }
}
