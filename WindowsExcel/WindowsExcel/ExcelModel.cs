using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public class ExcelModel
    {
        public string ExPeriod { get; set; }
        public DateTime? ExDate { get; set; }

        public string ExNo { get; set; }

        public string ExRoom { get; set; }

        public string ExProject { get; set; }

        public string ExItemNo { get; set; }
        public string ExItemName { get; set; }
        public string ExErrState { get; set; }
        public int? ExErrNum { get; set; }
        public string ExTypeCode { get; set; }
        public string ExUnit { get; set; }

        public double? ExPrice { get; set; }
        public double? ExErrMoney { get; set; }
    }
}
