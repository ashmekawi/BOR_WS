using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.BackEnd
{
    public class Book
    {
        public int ResponseCode { get; set; }
        public string ResponeMSG { get; set; }
        public List<BookRow> BookRow { get; set; }
        public Footer Footer { get; set; }
        public Header Header { get; set; }

    }
}