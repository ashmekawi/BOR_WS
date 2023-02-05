using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class GetCompanyInfoNIDRequest
    {
        [DataMember(IsRequired = true)]
        public int RequestSourse { get; set; }
        [DataMember(IsRequired = true)]
        public string citizenNationalId { get; set; }
    }
}