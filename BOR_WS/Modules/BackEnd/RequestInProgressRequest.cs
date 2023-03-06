using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.BackEnd
{
    public class RequestInProgressRequest
    {
        public int RequestID { get; set; }
        public int InProgress { get; set; }
        public int UserID { get; set; }
    }
}