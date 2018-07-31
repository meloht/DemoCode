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
    }
}
