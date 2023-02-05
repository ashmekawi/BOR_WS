using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class AddRequestRequest
    {
        public int RequestTypeID { get; set; }
        public string RequestXML { get; set; }
        public int InProgressID { get; set; }
        public int CustomerID { get; set; }
    }
}