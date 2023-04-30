using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetLookup
{
    public class AddOwnerLookUps
    {
        public List<Lookup> RightVote { get; set; }
        public List<Lookup> PerIDType { get; set; }
        public List<Lookup> NATIONALITY { get; set; }
        public List<Lookup> Contactype { get; set; }
        public List<Lookup> Bank { get; set; }
        public List<Lookup> DOCType { get; set; }
        public List<Lookup> ShareType { get; set; }
        public List<Lookup> PersonAdrsType { get; set; }
        public List<Lookup> Stockmarket { get; set; }
        public List<Lookup> legalposition { get; set; }
    }
}