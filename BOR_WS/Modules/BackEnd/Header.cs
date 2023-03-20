using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.BackEnd
{
    public class Header
    {
        public List<HeaderRow> HeaderRow { get; set; }
        public string QRCode { get; set; }
        public string QREXT { get; set; }
    }
}