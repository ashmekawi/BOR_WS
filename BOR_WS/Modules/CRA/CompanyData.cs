using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class CompanyData
    {
        public int CRA_NUM { get; set; }
        public string TAX_NUM { get; set; }
        public string DATE0 { get; set; }
        public decimal UCR { get; set; }
        public List<Capital> capitals { get; set; }
        
    }
}