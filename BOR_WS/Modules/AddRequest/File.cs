using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class File
    {
        public int RequestID { get; set; }
        public int Seq { get; set; }
        public string FData { get; set; }
        public string FExtType { get; set; }
        public int DocTypeID { get; set; }
    }
}

