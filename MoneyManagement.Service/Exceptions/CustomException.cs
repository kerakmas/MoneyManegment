using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManagement.Service.Exceptions
{
    public class CustomException : Exception
    {
        public int Code { get; set; }
        public CustomException(int code = 500, string message = "Something went wrong") : base(message)
        {
            this.Code = code;
        }
    }
}
