using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.CRA
{
    public class Companies
    {
        public string UCR { get; set; }
        public int CRA { set; get; }
        public int RegNum { set; get; }
        public string OfficeName { set; get; }
        public int OfficeCode { set; get; }
        public string Governorate { set; get; }
        public int GovernorateCode { set; get; }
        public string uid { set; get; }
        public string companyName { set; get; }
        public string position { set; get; }
        public string NextRenewalDate { set; get; }
        public string RegCreationDate { set; get; }
        public int signStatus { set; get; }

        public int legalType { set; get; }
        public int companySecurityApprovalFlag { set; get; }

        public int companystatusFlag { set; get; }
        public int ClassCode { set; get; }
    }
}