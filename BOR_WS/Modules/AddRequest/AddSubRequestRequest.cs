﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.AddRequest
{
    public class AddSubRequestRequest
    {
        public int RequestID { get; set; }
        public int SubRequestTypeID { get; set; }
        public string Txt { get; set; }
    }
}