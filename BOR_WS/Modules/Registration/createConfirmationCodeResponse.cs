using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    public class createConfirmationCodeResponse
    {
        public int ID { get; set; }
        public string ADesc { get; set; }
        public string LDesc { get; set; }
        public decimal RecID { get; set; }
        public int ConfirmCode { get; set; }
    }
}