using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class GetRequestFeesRequest
    {
        public int RequestTypeID { get; set; }
        public int IssueOfc { get; set; }
        public int DelvaryOpation { get; set; }
        public string XML { get; set; }

    }
}