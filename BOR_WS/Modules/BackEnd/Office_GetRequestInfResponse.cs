using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.BackEnd
{
    public class Office_GetRequestInfResponse
    {
        public List<Office_GetRequestInfo> RequestInfo { get; set; }
        public List<Decision> Decisions { get; set; }
        public List<RequestFile> Files { get; set; }
    }
}