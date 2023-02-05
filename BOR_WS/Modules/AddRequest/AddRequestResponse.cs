using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class AddRequestResponse
    {
        public int RequestID { get; set; }
        public int ResponseCode { get; set; }
        public string responseMSG { get; set; }
    }
}