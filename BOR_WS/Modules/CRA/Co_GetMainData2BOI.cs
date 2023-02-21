using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class Co_GetMainData2BOI
    {
        public decimal BOI__UCR { get; set; }
        public string BOI__RegDate { get; set; }
        public string BOI_TaxRegDate { get; set; }
        public string BOI__IssueLawDesc { get; set; }
        public string BOI__ISIC4 { get; set; }
        public string BOI__ActivityDesc { get; set; }
        public string BOI__TaxIdNum { get; set; }
        public string BOI__LegalDesc { get; set; }
        public string BOI__ConName { get; set; }
        public string BOI__AdrsGov { get; set; }
        public string BOI__AdrsPolice { get; set; }
        public string BOI__AdrsStreet { get; set; }

        public List<Co_GetCapital2BOI> Co_GetCapital2BOI { get; set; }



       
    }
}