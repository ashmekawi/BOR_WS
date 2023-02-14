using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class GetCompanyDataResponse
    {
        public CompanyData Company { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}