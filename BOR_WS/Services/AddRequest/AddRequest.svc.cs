using BOR_WS.DataBase;
using BOR_WS.EXTFUN;
using BOR_WS.Modules.AddRequest;
using BOR_WS.Modules.GetLookup;
using BOR_WS.ValidationManager.AddRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BOR_WS.Services.AddRequest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddRequest" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddRequest.svc or AddRequest.svc.cs at the Solution Explorer and start debugging.
    public class AddRequest : IAddRequest
    {
        CRRB_ServiceContext servicedb = new CRRB_ServiceContext();
        DBMan db = new DBMan();
        public AddRequestResponse AddNewRequest(AddRequestRequest request)
        {

            AddRequestResponse response = new AddRequestResponse();
            if (request.RequestTypeID == 2)
            {
                XDocument xdoc = XDocument.Parse(request.RequestXML);
                var UCR = xdoc.Descendants("CRAInfo__UCR").First()?.Value;
                int x = Fun.CRATOBOIRequest(UCR,request.CustomerID);
                AddSubRequestRequest addSub = new AddSubRequestRequest();
                addSub.RequestID = x;
                addSub.Txt = request.RequestXML;
                addSub.SubRequestTypeID = 2;
                AddSubRequest(addSub);
                response.RequestID = Convert.ToInt32(x);
                response.ResponseCode = 200;
                response.responseMSG = "تم التنفيذ";
                return response; 
            }
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
                string str = "SELECT * FROM [dbo].[Request_GetMyRequest] ('" + request.UserID + "',0,0) order by ID desc";
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
                        XDocument xdoc = XDocument.Parse(dbRow["RequestXML"].ToString());
                        try
                        {
                            request1.Name0 = xdoc.Descendants("BOI_Name__CoName").First()?.Value;
                        }
                        catch (Exception)
                        {
                            request1.Name0 = "لا يوجد";


                        }
                       
                        //request1.Name0 = GetRequestName(request1.ID,"الإسم/السمة");

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
        public Request GetRequestByID(int id)
        {
            try
            {
                string str = "SELECT CRRB_Request.ID, InProgress.Adesc as InProgressDesc, CRRB_Request.RequestTypeID, CRRB_Request.RequestXML, CRRB_Request.InProgressID, CRRB_Request.CustomerID, CRRB_Request.CreateDate, CRRB_Request.officecode, RequestType.Adesc AS RequestTypeDesc FROM            CRRB_Request INNER JOIN    RequestType ON CRRB_Request.RequestTypeID = RequestType.ID" +
                    " INNER JOIN  InProgress ON CRRB_Request.InProgressID = InProgress.ID where CRRB_Request.ID='" + id + "'";
                GetRequestsResponse response = new GetRequestsResponse();
                Request request = new Request();
                DataSet dbSet = new DataSet();
                db.openDatabaseConnection();
                dbSet = db.executeQuery(str);
                db.closeDatabaseConnection();
                if (dbSet.Tables[0].Rows.Count > 0)
                {

                   
                        
                        DataRow dbRow = dbSet.Tables[0].Rows[0];
                        request.ID = Convert.ToInt32(dbRow["ID"]);
                        request.InProgressDesc = Convert.ToString(dbRow["InProgressDesc"].ToString());
                        request.RequestTypeDesc = Convert.ToString(dbRow["RequestTypeDesc"].ToString());
                        request.CreateDate = Convert.ToDateTime(dbRow["CreateDate"].ToString());
                    try
                    {
                        XDocument xdoc = XDocument.Parse(dbSet.Tables[0].Rows[0]["RequestXML"].ToString());
                        request.Name0 = xdoc.Descendants("BOI_Name__CoName").First()?.Value;
                    }
                    catch (Exception)
                    {
                         
                        
                    }
                  
                    //request.Name0 = GetRequestName(request.ID, "الإسم/السمة");

                        
                    
                    db.closeDatabaseConnection();
                  
                    return request;
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
        public GetRequestHistoryResponse GetRequestHistory(int requestid)
        {
            GetRequestHistoryResponse response = new GetRequestHistoryResponse();
            response.request = GetRequestByID(requestid);

            try
            {
                string str = "SELECT * FROM [dbo].[Request_GetRequestAudit] ("+ requestid +")";
               
                List<RequestHistory> list = new List<RequestHistory>();
                DataSet dbSet = new DataSet();
                db.openDatabaseConnection();
                dbSet = db.executeQuery(str);
                db.closeDatabaseConnection();
                if (dbSet.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dbSet.Tables[0].Rows.Count; i++)
                    {
                        RequestHistory request1 = new RequestHistory();
                        DataRow dbRow = dbSet.Tables[0].Rows[i];
                        request1.InProgressID = Convert.ToInt32(dbRow["InProgressID"]);
                        request1.Seq = Convert.ToInt32(dbRow["Seq"].ToString());
                        request1.indate = Convert.ToDateTime(dbRow["indate"].ToString());
                        request1.IDTypeDesc = Convert.ToString(dbRow["IDTypeDesc"].ToString());
                      

                        list.Add(request1);
                    }
                    db.closeDatabaseConnection();
                    response.requestHistories = list;
                    return response;
                }
                else
                {
                    db.closeDatabaseConnection();
                    return response;
                }
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
        }
        public int RequestFiles(List<File> files)
        {
            foreach (var item in files)
            {
                string cmd= "[SP_CRRB_RequestAttachment_AddDoc]  @RequestID, @Seq  , @Fdata  , @FExtType  , @DocTypeID  , @ID_Number  , @NationalityID ";
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@RequestID";
                sqlParameter.Value = item.RequestID;
                SqlParameter sqlParameter2 = new SqlParameter();
                sqlParameter2.ParameterName = "@Seq";
                sqlParameter2.Value = item.Seq;
                SqlParameter sqlParameter3 = new SqlParameter();
                sqlParameter3.ParameterName = "@Fdata";
                sqlParameter3.Value = item.FData;
                SqlParameter sqlParameter4 = new SqlParameter();
                sqlParameter4.ParameterName = "@FExtType";
                int x = servicedb.Database.SqlQuery<int>("SELECT [ID] FROM [CRRB_Service].[dbo].[FExtType] where Adesc = '" + item.FExtType + "' ").FirstOrDefault();
                sqlParameter4.Value =x;
                SqlParameter sqlParameter5 = new SqlParameter();
                sqlParameter5.ParameterName = "@DocTypeID";
                sqlParameter5.Value = item.DocTypeID;
                SqlParameter sqlParameter6 = new SqlParameter();
                sqlParameter6.ParameterName = "@NationalityID";
                sqlParameter6.Value = 0;
                SqlParameter sqlParameter7 = new SqlParameter();
                sqlParameter7.ParameterName = "@ID_Number";
                sqlParameter7.Value = "0";
                sqlParameters.Add(sqlParameter);
                sqlParameters.Add(sqlParameter2);
                sqlParameters.Add(sqlParameter3);
                sqlParameters.Add(sqlParameter4);
                sqlParameters.Add(sqlParameter5);
                sqlParameters.Add(sqlParameter6);
                sqlParameters.Add(sqlParameter7);
                try
                {
                    var xx = servicedb.Database.ExecuteSqlCommand(cmd, sqlParameters.ToArray());
                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }

               
            }
            return 1;
        }
    }
}





