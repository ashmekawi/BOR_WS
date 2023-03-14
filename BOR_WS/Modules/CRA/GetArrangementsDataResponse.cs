using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class GetArrangementsDataResponse
    {
        public int BOIID { get; set; }
        public decimal UCR { get; set; }
        public int EntityTypeID { get; set; }
        public string EntityTypeDesc { get; set; }
        public int? Conametypeid { get; set; }
        public string COName { get; set; }
        public int? Reg_no { get; set; }
        public string OfficeName { get; set; }
        public DateTime? Date0 { get; set; }
        public int? LegalID { get; set; }
        public string LegalDesc { get; set; }
        public int IssueLawID { get; set; }
        public string IssueLawDesc { get; set; }

    }
}