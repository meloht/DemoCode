using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public static void ShowProcessing(string msg, Form owner, ParameterizedThreadStart work, object workArg = null)
        {
            FrmProcessing processingForm = new FrmProcessing(msg);
            dynamic expObj = new ExpandoObject();
            expObj.Form = processingForm;
            expObj.WorkArg = workArg;
            processingForm.SetWorkAction(work, expObj);
            processingForm.ShowDialog(owner);
            if (processingForm.WorkException != null)
            {
                throw processingForm.WorkException;
            }
        }


    }
}
