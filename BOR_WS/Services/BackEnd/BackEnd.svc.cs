using BOR_WS.EXTFUN;
using BOR_WS.Modules.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.BackEnd
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BackEnd" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BackEnd.svc or BackEnd.svc.cs at the Solution Explorer and start debugging.
    public class BackEnd : IBackEnd
    {
        CRA00Context db = new CRA00Context();

        public LoginResponse Login(LoginRequest request)
        {
            string str = "SELECT FULL_NAME as UserName ,[NUMBER0] as UserID FROM [CRA00].[dbo].[OPERATOR] where NAME=N'"+request.UserName+"' and Password= hashbytes('SHA1',N'" + request.Password + "')  and status = 'enable' and officecode = 200";
            LoginResponse response = db.Database.SqlQuery<LoginResponse>(str).FirstOrDefault();
            return response;
        }
    }
}
