using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class GetCompanyInfoNIDResponse
    {
        public List<Companies> Companies { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}