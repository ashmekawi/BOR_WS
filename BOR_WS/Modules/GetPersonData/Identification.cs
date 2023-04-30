using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class Identification
    {

        public int ID { get; set; }
        public int PersonID { get; set; }
        public int IDTypeID { get; set; }
        public int IDIssueSourceID { get; set; }
        public string IDTypeDesc { get; set; }
        public string IdIssueSourceDesc { get; set; }
        public string IDIssueSourceName { get; set; }
        public string IDNumber { get; set; }
        public string IssueDate { get; set; }
        public string IDExpireDate { get; set; }
        public byte RecStatus { get; set; }
    }
}