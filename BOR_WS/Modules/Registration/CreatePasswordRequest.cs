using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    public class CreatePasswordRequest
    {
        public int CustID { get; set; }
        public string Paswrd { get; set; }
        public int Confirmcode { get; set; }
    }
}