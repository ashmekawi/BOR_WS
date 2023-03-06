using BOR_WS.Modules.CRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.CRA
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetLegals" in both code and config file together.
    [ServiceContract]
    public interface IGetLegals
    {
        [OperationContract]
        GetCompanyInfoNIDResponse GetCompanyInfoNIDResponse(GetCompanyInfoNIDRequest GetCompanyInfoNIDRequest);
        [OperationContract]
        GetCompanyDataResponse GetCompanyDataResponse(string UCR);
        [OperationContract]

        Co_GetMainData2BOI GetMainData2BOI(string UCR);
        [OperationContract]
        GetBOIResponse GetBOI(string UCR);
        [OperationContract]
        List<Arrng> GetArrangements(string citizenNationalId);
    }
}
