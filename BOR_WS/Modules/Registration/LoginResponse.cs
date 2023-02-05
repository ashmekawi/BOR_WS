using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    public class LoginResponse
    {
        public int ID { get; set; }
        public string Adesc { get; set; }
        public string Ldesc { get; set; }
        public decimal RecID { get; set; }
        public string CustName { get; set; }
        public string NID { get; set; }

    }
}