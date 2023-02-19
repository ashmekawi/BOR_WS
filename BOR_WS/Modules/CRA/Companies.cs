using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class Companies
    {
        public int BOIID { get; set; }
        public string CoName { get; set; }
        public int Registration_No { get; set; }
        public string OfficeName { get; set; }
        public decimal UCR { get; set; }
        public string Date0 { get; set; }
        public int RenStatus { get; set; }
        public int BorStatus { get; set; } 
    }
}