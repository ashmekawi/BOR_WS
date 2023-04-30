using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    public class createConfirmationCodeRequest
    {
        public string NID { get; set; }
        public string Phone { get; set; }
    }
}