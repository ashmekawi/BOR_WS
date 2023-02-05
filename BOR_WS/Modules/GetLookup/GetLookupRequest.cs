using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetLookup
{
    public class GetLookupRequest
    {
        public string TblName { get; set; }
        public int CondValue { get; set; }
    }
}