using BOR_WS.Modules.AddRequest;
using BOR_WS.Modules.GetLookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.AddRequest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAddRequest" in both code and config file together.
    [ServiceContract]
    public interface IAddRequest
    {
        [OperationContract]
        AddRequestResponse AddNewRequest(AddRequestRequest request);
        [OperationContract]
        AddSubRequestResponse AddSubRequest(AddSubRequestRequest request);
        [OperationContract]
        int RequestCount(int UserID);
        [OperationContract]
        GetRequestsResponse GetRequests(GetRequestsRequest request);
        [OperationContract]
        GetRequestHistoryResponse GetRequestHistory(int requestid);
     





    }
}
