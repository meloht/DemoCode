using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public class LineItems<T>
    {
        public List<string> XPoints { get; set; }

        public List<T> YPoints { get; set; }

        public DataTable Table { get; set; }

        public string YLable { get; set; }

        public string XLable { get; set; }
    }
}
