using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class Capital
    {
        public decimal Value { get; set; }
        public Int16 Code { get; set; }
        public string Desc { get; set; }
        public Int16 CurrencyCode { get; set; }
        public string CurrencyDesc { get; set; }

    }
}