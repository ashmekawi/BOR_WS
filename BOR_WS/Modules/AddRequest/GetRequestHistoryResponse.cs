using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class GetRequestHistoryResponse
    {
        public Request request { get; set; }
        public List<RequestHistory> requestHistories { get; set; }
    }
}