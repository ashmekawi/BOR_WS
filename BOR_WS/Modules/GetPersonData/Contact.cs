using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class Contact
    {
        public decimal ID { get; set; }
        public decimal PersonID { get; set; }
        public int ContactTypeID { get; set; }
        public string ContactTypeDesc { get; set; }
        public string ContactData { get; set; }
        public byte RecStatus { get; set; }
    }
}