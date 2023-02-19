using BOR_WS.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace BOR_WS.EXTFUN
{
    public class Fun
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static void Sms(string msg, string phone)
        {
            string id = "";
            DBMan db = new DBMan();
            DataSet result = new DataSet();
            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_SMS_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ID", 0);
                sqlCommand.Parameters.AddWithValue("@Phone", phone);
                sqlCommand.Parameters.AddWithValue("@MSG", msg);
                sqlCommand.Parameters.AddWithValue("@Result", "");
                sqlCommand.Parameters.AddWithValue("@UserID", 0);
                sqlCommand.Parameters.AddWithValue("@New", 1);
                sqlCommand.Connection = db.databaseConnection;
                result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
                id = result.Tables[0].Rows[0][0].ToString();
            }

            catch (Exception ex)
            {

            }

            phone = "2" + phone;
            string network = phone.Substring(0, 4);

            string Oprator="";
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
                    break;

            }
            msg = "رقم التفعيل الخاص بكم هو" + msg;
            string url = @"http://bulksms.advansystelecom.com/Message_Request.aspx?" +
                "username=ITDA&password=!TD@P$D&request_id=" + id + "&Mobile_No=" + phone + "&type=2" +
                "&message=" + msg + "&encoding=2&sender=ITDA" +
                "&operator=" + Oprator + "";
            var client = new WebClient();
            var content = "";
            try
            {
                 content = client.DownloadString(url);

            }
            catch (Exception ex)
            {

                content = ex.Message;
            }
           


            try
            {
                db.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_SMS_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(result.Tables[0].Rows[0][0]));
                sqlCommand.Parameters.AddWithValue("@Phone", phone);
                sqlCommand.Parameters.AddWithValue("@MSG", msg);
                sqlCommand.Parameters.AddWithValue("@Result", content);
                sqlCommand.Parameters.AddWithValue("@UserID", 0);
                sqlCommand.Parameters.AddWithValue("@New", 0);
                sqlCommand.Connection = db.databaseConnection;
                result = db.executeSqlCommand(sqlCommand);
                db.closeDatabaseConnection();
            }

            catch (Exception ex)
            {

            }
         
        }

        public static DataTable XMLTODataTable(string str)
        {
            string xmlData = str;

            XElement x = XElement.Parse(xmlData);

            DataTable dt = new DataTable();

            XElement setup = (from p in x.Descendants() select p).First();

            foreach (XElement xe in setup.Descendants()) // build your DataTable
                dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt

            var all = from p in x.Descendants(setup.Name.ToString()) select p;

            foreach (XElement xe in all)
            {
                DataRow dr = dt.NewRow();
                foreach (XElement xe2 in xe.Descendants())
                    dr[xe2.Name.ToString()] = xe2.Value; //add in the values
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static int CRATOBOIRequest(string UCR,int UserID)
        {
            CRADB db = new CRADB();
            try
            {
              
                db.openDatabaseConnection();
                string str = "SELECT [dbo].[CRA00_GetCRANum] ('"+UCR+"')";
                DataSet result = db.executeQuery(str);
                db.closeDatabaseConnection();
                string xml = "select * from [dbo].[CRA00_GetCoData2CRRB_Request]('"+ result.Tables[0].Rows[0][0] +"')";
                db.openDatabaseConnection();
                DataSet xmlresult = db.executeQuery(xml);
                db.closeDatabaseConnection();
                DBMan dBMan = new DBMan();
                dBMan.openDatabaseConnection();
                SqlCommand sqlCommand = new SqlCommand("[sp_CRRB_Request_ADD]");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@RequestTypeID", 2);
                sqlCommand.Parameters.AddWithValue("@OfficeCode", 0);
                sqlCommand.Parameters.AddWithValue("@RequestXML", xmlresult.Tables[0].Rows[0][0].ToString());
                sqlCommand.Parameters.AddWithValue("@CustomerID", UserID);
             
                sqlCommand.Connection = dBMan.databaseConnection;
                DataSet result1 = dBMan.executeSqlCommand(sqlCommand);
                dBMan.closeDatabaseConnection();
                return Convert.ToInt32(result1.Tables[1].Rows[0][0]);
                
            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return 1;
        }
    }
}