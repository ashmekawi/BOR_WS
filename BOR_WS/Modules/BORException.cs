using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules
{
    public class BORException : Exception
    {
        public int code { set; get; }
        public string message { set; get; }
        public BORException(int code, string message)
           : base(message)
        {
            this.code = code;
            this.message = message;

        }
    }
}