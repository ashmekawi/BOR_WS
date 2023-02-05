using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.DataBase
{
    public class DBException: Exception
    {
        public int code { set; get; }
        public string message { set; get; }
        public DBException(int code, string message)
           : base(message)
        {
            this.code = code;
            this.message = message;

        }
    }
}