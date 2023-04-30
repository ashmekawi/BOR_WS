using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class PersonMainData
    {
        public decimal ID { get; set; }
        public string PersonTitelDesc { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string MyProperty { get; set; }
        public string LName1 { get; set; }
        public string LName2 { get; set; }
        public string LName3 { get; set; }
        public string LName4 { get; set; }
        public int BirthDate { get; set; }
        public int NationDesc  { get; set; }
        public int GovBirth { get; set; }
        public int PoliceBirth { get; set; }
        public string IdTypeDesc { get; set; }
        public string IDNumber { get; set; }
        public int NATIONALITYID { get; set; }
    }
}