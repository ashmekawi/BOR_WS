using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class Request
    {
        public int ID { get; set; }
        public string RequestTypeDesc { get; set; }
        public string InProgressDesc { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name0 { get; set; }

    }
}