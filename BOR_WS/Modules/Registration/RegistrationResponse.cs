using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    public class RegistrationResponse
    {
        public int ID { get; set; }
        public string Adesc { get; set; }
        public string Ldesc { get; set; }
        public decimal RecID { get; set; }
        public int Confirmcode { get; set; }
        public int IsConfirmed { get; set; }
    }
}