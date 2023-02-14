using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class RequestHistory
    {
        public int Seq { get; set; }
        public int InProgressID { get; set; }
        public DateTime indate { get; set; }
        public string IDTypeDesc { get; set; }

    }
}