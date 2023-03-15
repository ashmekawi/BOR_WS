using BOR_WS.Modules;
using BOR_WS.Modules.AddRequest;
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
        [OperationContract]
        int AddEntity(AddEntityRequest request);
        [OperationContract]
        List<BackEndRequestsReponse> Requests(int Office, int UserID, int InProgress);
        [OperationContract]
        List<Decision> AvailableDecision(int RequestID, int DecisionMaker);
        [OperationContract]
        List<Request> GetBackEndRequest(int RequestID, string Nid, string Phon, string UCR);
        [OperationContract]
        Office_GetRequestInfResponse GetBackEndRequestData(int RequestID);
        [OperationContract]
        Book GetBOIBook(string BOIID,string UCR);
        [OperationContract]
        Result Payment(int RequestID, decimal TotalFees, int ReceiptNum, int ReceiptGroup, DateTime ReceiptDate, int UserID);
    }
}
