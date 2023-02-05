using BOR_WS.Modules;
using BOR_WS.Modules.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.ValidationManager
{
    public class RegistrationValidation
    {
        public void Validate(RegistrationRequest request)
        {
            if (string.IsNullOrEmpty(request.FristName)) throw new BORException(301, "Frist Name Is required");
            if (string.IsNullOrEmpty(request.SecondName)) throw new BORException(302, "Second Name Is required");
            if (string.IsNullOrEmpty(request.ThirdName)) throw new BORException(303, "Third Name Is required");
            if (string.IsNullOrEmpty(request.FourthName)) throw new BORException(304, "Fourth Name Is required");
            if (string.IsNullOrEmpty(request.SurName)) throw new BORException(305, "Sur Name Is required");
            if (string.IsNullOrEmpty(request.IDNumber)) throw new BORException(306, "ID Number Is required");
            if (request.IDNumber.Length!=14) throw new BORException(307, "ID Number Must be 14 Digit");
            foreach (char c in request.IDNumber)
            {
                if (!char.IsDigit(c))
                {
                    throw new BORException(308, "الرقم التعريفي يجب أن يتكون من أرقام فقط");
                }
            }
            if (string.IsNullOrEmpty(request.Phone)) throw new BORException(309, "Phone Number Is required");
            if (request.Phone.Length!=11) throw new BORException(310, "Phone Number Not Valid");
            foreach (char c in request.Phone)
            {
                if (!char.IsDigit(c))
                {
                    throw new BORException(311, "رقم الهاتف يجب أن يتكون من أرقام فقط");
                }
            }
        }
    }
}