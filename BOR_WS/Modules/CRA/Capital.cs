using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class Capital
    {
        public decimal Value { get; set; }
        public int Code { get; set; }
        public string Desc { get; set; }
        public int CurrencyCode { get; set; }
        public string CurrencyDesc { get; set; }

    }
}