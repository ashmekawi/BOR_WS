using BOR_WS.DataBase;
using BOR_WS.EXTFUN;
using BOR_WS.Modules.Registration;
using BOR_WS.ValidationManager;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.Registration
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Registration" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Registration.svc or Registration.svc.cs at the Solution Explorer and start debugging.
    public class Registration : IRegistration
    {
        DBMan db = new DBMan();
        public CreatePasswordResponse CreatrePassword(CreatePasswordRequest request)
        {
            CreatePasswordResponse response = new CreatePasswordResponse();
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_CustomerUpdatePasword]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CustID", request.CustID);
                sqlCommand.Parameters.AddWithValue("@Paswrd", request.Paswrd);
                sqlCommand.Parameters.AddWithValue("@Confirmcode", request.Confirmcode);
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response = Fun.GetItem<CreatePasswordResponse>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return response;
        }
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [dbo].[Customers_GetUserInfo] (@Phone,@CustPassWord)");
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Phone", request.Phone);
                sqlCommand.Parameters.AddWithValue("@CustPassWord", request.Pass);
                sqlCommand.Connection = db.databaseConnection;

                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response = Fun.GetItem<LoginResponse>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                response.ID = 0;
                response.Ldesc = ex.Message;
                return response;
            }
            return response;

        }
        public RegistrationResponse registration(RegistrationRequest request)
        {
            RegistrationValidation validation = new RegistrationValidation();

            validation.Validate(request);


            RegistrationResponse response = new RegistrationResponse();
            try
            {
                string name0 = request.FristName + " " + request.SecondName + " " + request.ThirdName + " " + request.FourthName;
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_Customers_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@NID", request.IDNumber);
                sqlCommand.Parameters.AddWithValue("@Name1", request.FristName);
                sqlCommand.Parameters.AddWithValue("@Name2", request.SecondName);
                sqlCommand.Parameters.AddWithValue("@Name3", request.ThirdName);
                sqlCommand.Parameters.AddWithValue("@Name4", request.FourthName);
                sqlCommand.Parameters.AddWithValue("@IDType", 1);
                sqlCommand.Parameters.AddWithValue("@NATION_TYPEID", 1);
                sqlCommand.Parameters.AddWithValue("@NATIONALITYID", 13818);
                sqlCommand.Parameters.AddWithValue("@Phone", request.Phone);
                sqlCommand.Parameters.AddWithValue("@CustPassWord", "Null");
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response = Fun.GetItem<RegistrationResponse>(result.Tables[0].Rows[0]);
                if(response.ID == -2&&response.IsConfirmed==0)
                {
                    DataSet result1 = new DataSet();
                    string str = "SELECT [ID],[Phone],[ConfirmCode]FROM [CRRB_Service].[dbo].[Customers] where Phone='" + request.Phone + "'";
                    try
                    {
                        db.openDatabaseConnection();
                        SqlCommand sqlCommand1 = new SqlCommand(str);
                        sqlCommand1.CommandType = CommandType.Text;
                        sqlCommand1.Connection = db.databaseConnection;
                        result1 = db.executeSqlCommand(sqlCommand1);
                        db.closeDatabaseConnection();
                        response.ID = -2;
                        response.Confirmcode = Convert.ToInt32(result1.Tables[0].Rows[0]["ConfirmCode"]);
                        Fun.SmsITDA(response.Confirmcode.ToString(),request.Phone);
                        return response;
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (response.ID == 1)
                {
                    Fun.SmsITDA(Convert.ToString(response.Confirmcode),request.Phone );
                }
              

            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return response;
        }
        public bool ValedConfirmationCode(string ConfimationCode, string NID)
        {
            string code = "";
            int VLD = 0;
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("SELECT ID,isnull(ConfirmCode,0) FROM [dbo].[Customers_GetInfoByNID] ('" + NID + "')");
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                code = result.Tables[0].Rows[0][1].ToString();
                VLD = Convert.ToInt32(result.Tables[0].Rows[0][0].ToString());
                db.closeDatabaseConnection();
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            if (ConfimationCode == code && VLD == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ReSendConfirmation ReSendConfirmationCode(string Mob)
        {
            DataSet result = new DataSet();
            string str = "SELECT [ID],[Phone],[ConfirmCode]FROM [CRRB_Service].[dbo].[Customers] where Phone='"+Mob+"'";
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand(str);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = db.databaseConnection;
                result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
            }
            catch (Exception)
            {

                throw;
            }
            ReSendConfirmation response = new ReSendConfirmation();
            response.ID = Convert.ToInt32(result.Tables[0].Rows[0]["ID"]);
            response.Confirmcode = Convert.ToInt32(result.Tables[0].Rows[0]["Confirmcode"]);
            Fun.SmsITDA(Convert.ToString(response.Confirmcode),Mob);

            return response;

        }

    }
}
