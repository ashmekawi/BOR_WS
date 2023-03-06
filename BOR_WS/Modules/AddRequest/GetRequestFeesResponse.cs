using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class GetRequestFeesResponse
    {
        public string AccountNum { get; set; }
        public string FeeName { get; set; }
        public decimal Amount { get; set; }
    }
}