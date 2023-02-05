using BOR_WS.DataBase;
using BOR_WS.EXTFUN;
using BOR_WS.Modules.AddRequest;
using BOR_WS.ValidationManager.AddRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.AddRequest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddRequest" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddRequest.svc or AddRequest.svc.cs at the Solution Explorer and start debugging.
    public class AddRequest : IAddRequest
    {
        DBMan db = new DBMan();
        public AddRequestResponse AddNewRequest(AddRequestRequest request)
        {

            AddRequestResponse response = new AddRequestResponse();


            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_CRRB_Request_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RequestTypeID", request.RequestTypeID);
                sqlCommand.Parameters.AddWithValue("@OfficeCode", 0);
                sqlCommand.Parameters.AddWithValue("@RequestXML", request.RequestXML);
                sqlCommand.Parameters.AddWithValue("@CustomerID", request.CustomerID);
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response.RequestID = Convert.ToInt32(result.Tables[1].Rows[0][0]);
                response.ResponseCode = 200;
                response.responseMSG = "تم التنفيذ";
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return response;
        }

        public AddSubRequestResponse AddSubRequest(AddSubRequestRequest request)
        {
            AddSubRequestResponse response = new AddSubRequestResponse();
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_CRRB_RequestData_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RequestID", request.RequestID);
                sqlCommand.Parameters.AddWithValue("@SubReqursTypeID", request.SubRequestTypeID);
                sqlCommand.Parameters.AddWithValue("@Txt", request.Txt);
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response.RequestID = Convert.ToInt32(result.Tables[0].Rows[0][0]);
                response.ResponseCode = 200;
                response.responseMSG = "تم التنفيذ";
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return response;
        }

        public int RequestCount(int UserID)
        {

            string str = "select count(*) from [CRRB_Request] where [CustomerID]='" + UserID + "' and [InProgressID] = 101";
            DataSet dbSet = new DataSet();

            db.openDatabaseConnection();
            dbSet = db.executeQuery(str);
            db.closeDatabaseConnection();

            return Convert.ToInt32(dbSet.Tables[0].Rows[0][0]);
        }



        public GetRequestsResponse GetRequests(GetRequestsRequest request)
        {

            try
            {
                string str = "SELECT ID,RequestTypeDesc,InProgressDesc,CreateDate FROM [dbo].[Request_GetMyRequest] ('" + request.UserID + "',0,0) order by ID desc";
                GetRequestsResponse response = new GetRequestsResponse();
                List<Request> requests = new List<Request>();
                DataSet dbSet = new DataSet();
                db.openDatabaseConnection();
                dbSet = db.executeQuery(str);
                db.closeDatabaseConnection();
                if (dbSet.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dbSet.Tables[0].Rows.Count; i++)
                    {
                        Request request1 = new Request();
                        DataRow dbRow = dbSet.Tables[0].Rows[i];
                        request1.ID = Convert.ToInt32(dbRow["ID"]);
                        request1.InProgressDesc = Convert.ToString(dbRow["InProgressDesc"].ToString());
                        request1.RequestTypeDesc = Convert.ToString(dbRow["RequestTypeDesc"].ToString());
                        request1.CreateDate = Convert.ToDateTime(dbRow["CreateDate"].ToString());
                       

                        requests.Add(request1);
                    }
                    db.closeDatabaseConnection();
                    response.Requests = requests;
                    return response;
                }
                else
                {
                    db.closeDatabaseConnection();
                    throw new DBException(404, "لا يوجد منشئات لهذا الرقم القومي");
                }
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }



            
        }
    }
}





