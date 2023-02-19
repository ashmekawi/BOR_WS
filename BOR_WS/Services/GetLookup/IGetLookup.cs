using BOR_WS.Modules.GetLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.GetLookup
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetLookup" in both code and config file together.
    [ServiceContract]
    public interface IGetLookup
    {
        [OperationContract]
        GetLookupResponse GetLookupResponse(GetLookupRequest request);
        [OperationContract]
        AddOwnerLookUps AddOwnerLookUp();
    }
}
