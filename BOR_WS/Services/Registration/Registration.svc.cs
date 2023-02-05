﻿using BOR_WS.DataBase;
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
                sqlCommand.Parameters.AddWithValue("@Name0", name0);
                sqlCommand.Parameters.AddWithValue("@Phone", request.Phone);
                sqlCommand.Parameters.AddWithValue("@CustPassWord", "Null");
                sqlCommand.Connection = db.databaseConnection;
                DataSet result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                response = Fun.GetItem<RegistrationResponse>(result.Tables[0].Rows[0]);
                try
                {
                    SendSMS(request.Phone, Convert.ToString(response.Confirmcode),Convert.ToInt32(response.RecID));
                }
                catch (Exception ex )
                {

                    response.Ldesc = ex.Message;
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
            string code ="";
            int VLD = 0;
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("SELECT ID,isnull(ConfirmCode,0) FROM [dbo].[Customers_GetInfoByNID] ('"+ NID +"')");
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
        public bool SendSMS(string phone,string msg,int id)
        {
            phone = "2" + phone;
            string network = phone.Substring(0, 4);

            var x = sendsms(msg, phone, 3000000 + id, network);





            var client = new RestClient("https://api.ultramsg.com/instance28563/messages/chat");
            var request = new RestRequest("",Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("token", "tb6o11jpueg2ppvy");
            request.AddParameter("to", phone);
            request.AddParameter("body", msg);
            request.AddParameter("priority","1");
            request.AddParameter("referenceId", ParameterType.RequestBody);
            var response = client.Execute(request);
            return true;
        }


        public string sendsms(string msg, string mob, int id, string network)
        {
            string Oprator;
            switch (network)
            {
                case "2010":
                    Oprator = "Vodafone";
                    break;
                case "2012":
                    Oprator = "Mobinil";
                    break;
                case "2011":
                    Oprator = "Etisalat";
                    break;
                case "2015":
                    Oprator = "WE";
                    break;
                default:
                    return "Bad Network";

            }
            msg = "رقم التفعيل الخاص بكم هو" + msg;
            string url = @"http://bulksms.advansystelecom.com/Message_Request.aspx?" +
                "username=ITDA&password=!TD@P$D&request_id=" + id + "&Mobile_No=" + mob + "&type=2" +
                "&message=" + msg + "&encoding=2&sender=ITDA" +
                "&operator=" + Oprator + "";
            var client = new WebClient();
            var content = client.DownloadString(url);
            return content;
        }



      
    }
}
