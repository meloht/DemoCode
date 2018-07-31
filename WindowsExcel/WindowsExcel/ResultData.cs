using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsExcel
{
    public class ResultData<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrMessage { get; set; }
    }
}
