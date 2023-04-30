using BOR_WS.EXTFUN;
using BOR_WS.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BOR_WS
{
    /// <summary>
    /// Summary description for BackEndPrint
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BackEndPrint : System.Web.Services.WebService
    {
        CRRB_ServiceContext db = new CRRB_ServiceContext();
        CRA00Context cra = new CRA00Context();

        [WebMethod]
        public string PrinBook_BackEnd(string UCR, int Opt, int UserID)
        {
            int CustomerID =Convert.ToInt32(db.Database.SqlQuery<decimal>("select isnull(ID,0) from Customers where CRAUserID = " + UserID + "").FirstOrDefault());
            if (CustomerID == 0)
            {
                string GetUserData = "SELECT [FULL_NAME],[N_ID],[PHONE_NO] FROM [CRA00].[dbo].[OPERATOR] where NUMBER0 = " + UserID + "";
                CRAUser cRAUser = new CRAUser();
                cRAUser = cra.Database.SqlQuery<CRAUser>(GetUserData).FirstOrDefault();
                string str = "EXECUTE [dbo].[sp_Customers_ADD_FromCRA] " +
                             cRAUser.N_ID+
                             ",'"+cRAUser.FULL_NAME +"'"+
                             ",1" +
                             ",1" +
                             ",13818" +
                             ","+ cRAUser.PHONE_NO +
                             ",0" +
                             ","+UserID;
                CustomerID = Convert.ToInt32(db.Database.SqlQuery<decimal>(str).FirstOrDefault()); 
            }
            if (Opt == 1)
            {
                decimal BOIID = db.Database.SqlQuery<decimal>("select isnull(BOIID,0)" +
                    " from CRRB.dbo.BOI where UCR =" + UCR + " ").FirstOrDefault();
                if (BOIID == 0)
                {
                    try
                    {
                        save(UCR, CustomerID);
                    }
                    catch (Exception)
                    {


                    }

                }
                string str = "SELECT [CRRB].[dbo].[BOI_Book2XML] (" + UCR + ")";
                return db.Database.SqlQuery<string>(str).FirstOrDefault();
            }
            else if (Opt == 0)
            {
                return Convert.ToString(save(UCR, CustomerID));
            }
            return "";
        }

        public int save(string UCR, int CustomerID)
        {
            int ReqID = Fun.CRATOBOIRequest(UCR, CustomerID);
            string str = "exec [dbo].[sp_CRRB_AddEntity_New]  " + ReqID + "  ," + CustomerID + "";
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CRRB_ServiceContext"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            try
            {
                con.Open();
                da.Fill(ds);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                throw;
            }
            int x;
            try
            {
                x = Convert.ToInt32(ds.Tables[ds.Tables.Count].Rows[0]["RecID"]);

            }
            catch (Exception)
            {

                x = 0;
            }
            return x;
        }


    }
}
