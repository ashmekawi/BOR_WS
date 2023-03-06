using BOR_WS.Modules.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.BackEnd
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBackEnd" in both code and config file together.
    [ServiceContract]
    public interface IBackEnd
    {
        [OperationContract]
        LoginResponse Login(LoginRequest request);
        [OperationContract]
        int RequestInProgress(RequestInProgressRequest request);
        //[OperationContract]
        // int AddEntity(AddEntityRequest request);
    }
}
