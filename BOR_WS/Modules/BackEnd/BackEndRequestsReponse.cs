using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.BackEnd
{
    public class BackEndRequestsReponse
    {

        public decimal ID { get; set; }
        public int RequestTypeID { get; set; }
        public int InProgressID { get; set; }
        public decimal CustomerID { get; set; }
        public int NATIONALITYID { get; set; }
        public string RequestTypeDesc { get; set; }
        public string Name0 { get; set; }
        public string IDTypeDesc { get; set; }
        public string NID { get; set; }
        public string NationDesc { get; set; }
        public string InProgressDesc { get; set; }


    }
}