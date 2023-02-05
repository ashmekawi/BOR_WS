using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.ControlPanel
{
    public class ControlPanelResponse
    {
        public int RequestsCount { get; set; }
        public int BOCount { get; set; }
        public int LegalCount { get; set; }
        public int ArggCount { get; set; }
    }
}