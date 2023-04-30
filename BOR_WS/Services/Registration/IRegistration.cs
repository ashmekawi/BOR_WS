using BOR_WS.Modules.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.Registration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRegistration" in both code and config file together.
    [ServiceContract]
    [RequiredParametersBehavior]

    public interface IRegistration
    {
        [OperationContract]
        RegistrationResponse registration(RegistrationRequest request);
        [OperationContract]
        Boolean ValedConfirmationCode(string ConfimationCode,string NID);
        [OperationContract]
        CreatePasswordResponse CreatrePassword(CreatePasswordRequest request);
        [OperationContract]
        LoginResponse Login(LoginRequest request);
        //bool SendSMS(string phone, string msg);
        //string sendsms(string msg, string mob, int id, string network);
        [OperationContract]
        ReSendConfirmation ReSendConfirmationCode(string Mob);
        [OperationContract]
        createConfirmationCodeResponse CreateConfirmationCode(createConfirmationCodeRequest request);
    }
}
