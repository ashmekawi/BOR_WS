using BOR_WS.EXTFUN;
using BOR_WS.Modules.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        CRRB_ServiceContext CRRB = new CRRB_ServiceContext();
        public LoginResponse Login(LoginRequest request)
        {
            string str = "SELECT FULL_NAME as UserName ,[NUMBER0] as UserID FROM [CRA00].[dbo].[OPERATOR] where NAME=N'"+request.UserName+"' and Password= hashbytes('SHA1',N'" + request.Password + "')  and status = 'enable' and officecode = 200";
            LoginResponse response = db.Database.SqlQuery<LoginResponse>(str).FirstOrDefault();
            return response;
        }
        public int RequestInProgress(RequestInProgressRequest request)
        {
            string str = "sp_CRRB_RequestAudit_add "+request.RequestID+","+request.InProgress+","+request.UserID+"";
            CRRB.Database.ExecuteSqlCommand(str);
            int x = CRRB.Database.SqlQuery<int>("SELECT [InProgressID] FROM [CRRB_Service].[dbo].[CRRB_Request] where id=" + request.RequestID).FirstOrDefault();
            if (x == request.InProgress)
            {
                return 1;

            }
            else
            {
                return 0;
            }      
        }
        public int AddEntity(AddEntityRequest request)
        {
            string str = "exec [dbo].[sp_CRRB_AddEntity]  " + request.RequestID + "  ," + request.UserID + "";
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRRB_ServiceContext"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(str,con);
            try
            {
                con.Open();
                da.Fill(ds);
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
                throw;
            }

            return Convert.ToInt32(ds.Tables[ds.Tables.Count].Rows[0]["RecID"]);

        }
        public List<BackEndRequestsReponse> Requests(int Office, int UserID,int InProgress)
        {
            List<BackEndRequestsReponse> response = new List<BackEndRequestsReponse>();
            response = CRRB.Database.SqlQuery<BackEndRequestsReponse>("SELECT * FROM [dbo].[Request_GetAtInProgress]"+
                " ( "+Office+" ,"+UserID+"  ,"+InProgress+")").ToList();
            return response;

        }
        public List<Decision> AvailableDecision(int RequestID, int DecisionMaker)
        {
            List<Decision> Decisions = new List<Decision>();
            string str = "SELECT* FROM[dbo].[Request_AvailableDecision] ("+RequestID+","+DecisionMaker+")";
            Decisions = CRRB.Database.SqlQuery<Decision>(str).ToList();
            return Decisions;
        }
    }
}
