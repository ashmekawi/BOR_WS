using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class PerMGM
    {
        public decimal ID { get; set; }
        public decimal PersonID { get; set; }
        public int LegalPosionID { get; set; }
        public int MGMTStatus { get; set; }
        public int SignStatus { get; set; }
        public string PosionDesc { get; set; }
        public string MGMTDesc { get; set; }
        public string SignDesc { get; set; }
    }
}