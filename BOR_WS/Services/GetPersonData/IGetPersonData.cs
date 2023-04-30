using BOR_WS.Modules.GetPersonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.GetPersonData
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetPersonData" in both code and config file together.
    [ServiceContract]
    public interface IGetPersonData
    {
        [OperationContract]
       Person PersonData(int BOIID,int PersonID);
    }
}
