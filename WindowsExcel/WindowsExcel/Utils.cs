using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public class Utils
    {
        public static DateTime? ToDateTime(string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;
            DateTime date = DateTime.MinValue;
            if (DateTime.TryParse(s, out date))
            {
                return date;
            }
            return null;
        }

        public static double? ToDouble(string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;

            double num = 0;
            if (double.TryParse(s, out num))
            {
                return num;
            }
            return null;
        }
        public static int? ToInt(string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;

            int num = 0;
            if (int.TryParse(s, out num))
            {
                return num;
            }
            return null;
        }
    }
}
