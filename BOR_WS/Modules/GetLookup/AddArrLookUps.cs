using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetLookup
{
    public class AddArrLookUps
    {
        public List<Lookup> IssueLaw { get; set; }
        public List<Lookup> ArrType { get; set; }
        public List<Lookup> Permission { get; set; }
        public List<Lookup> Contact { get; set; }

    }
}