using BOR_WS.Modules;
using BOR_WS.Modules.AddRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.ValidationManager.AddRequest
{
    public class AddRequestValidation
    {
        public void Validate(AddRequestRequest request)
        {
            if (string.IsNullOrEmpty(request.RequestXML)) throw new BORException(301, "Request is null");
            if (request.RequestTypeID == 0) throw new BORException(302, "Second Name Is required");
            if (request.InProgressID == 0) throw new BORException(303, "Third Name Is required");
            if (request.CustomerID == 0) throw new BORException(304, "Fourth Name Is required");
        }
    }
}