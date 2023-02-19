using BOR_WS.DataBase;
using BOR_WS.EXTFUN;
using BOR_WS.Modules.CRA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.CRA
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetLegals" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetLegals.svc or GetLegals.svc.cs at the Solution Explorer and start debugging.
    public class GetLegals : IGetLegals
    {
        CRRB_ServiceContext db = new CRRB_ServiceContext();
        CRA00Context db1 = new CRA00Context();
        public GetCompanyInfoNIDResponse GetCompanyInfoNIDResponse(GetCompanyInfoNIDRequest GetCompanyInfoNIDRequest)
        {
            List<Companies> companies = new List<Companies>();
            companies = db.Database.SqlQuery<Companies>("SELECT * FROM [dbo].[CRRB_GetBOI_ByNID] ('" + GetCompanyInfoNIDRequest.citizenNationalId + "')").ToList();
            GetCompanyInfoNIDResponse response = new GetCompanyInfoNIDResponse();
            response.Companies = db1.Database.SqlQuery<Companies>("SELECT * FROM [dbo].[Fn_Por_GetDataByNID] ('" + GetCompanyInfoNIDRequest.citizenNationalId + "')").ToList();
            if (companies.Count > 0)
            {
                response.Companies.AddRange(companies);

            }
            return response;
        }
        public GetCompanyDataResponse GetCompanyDataResponse(string UCR)
        {
            GetCompanyDataResponse response = new GetCompanyDataResponse();
            response.Company = db1.Database.SqlQuery<CompanyData>("SELECT * FROM [dbo].[Fn_getCapitalByUCR] ('"+UCR+"')").FirstOrDefault();
            response.Company.capitals = db1.Database.SqlQuery<Capital>("SELECT * FROM [dbo].[Fn_getCapitalByUCR] ('" + UCR + "')").ToList();
            response.ResponseCode = 200;
            response.ResponseMessage = "Sucsess";
            return response;



        }
        public Co_GetMainData2BOI GetMainData2BOI(string UCR)
        {
            CRADB db = new CRADB();
            Co_GetMainData2BOI data2BOI = new Co_GetMainData2BOI();
            try
            {
                DataSet ds = new DataSet();
                db.openDatabaseConnection();
                ds = db.executeQuery("SELECT * FROM [dbo].[Co_GetMainData2BOI] ('" + UCR + "')");
                db.closeDatabaseConnection();
                data2BOI.BOI_TaxRegDate = Convert.ToString(ds.Tables[0].Rows[0]["BOI_TaxRegDate"]);
                data2BOI.BOI__ActivityDesc = Convert.ToString(ds.Tables[0].Rows[0]["BOI__ActivityDesc"]);
                data2BOI.BOI__ISIC4 = Convert.ToString(ds.Tables[0].Rows[0]["BOI__ISIC4"]);
                data2BOI.BOI__IssueLawDesc = Convert.ToString(ds.Tables[0].Rows[0]["BOI__IssueLawDesc"]);
                data2BOI.BOI__RegDate = Convert.ToString(ds.Tables[0].Rows[0]["BOI__RegDate"]);
                data2BOI.BOI__UCR = Convert.ToString(ds.Tables[0].Rows[0]["BOI__UCR"]);
                data2BOI.BOI__TaxIdNum = Convert.ToString(ds.Tables[0].Rows[0]["BOI__TaxIdNum"]);
                data2BOI.BOI__LegalDesc = Convert.ToString(ds.Tables[0].Rows[0]["BOI__LegalDesc"]);
                data2BOI.BOI__ConName = Convert.ToString(ds.Tables[0].Rows[0]["BOI__ConName"]);
                data2BOI.BOI__AdrsGov = Convert.ToString(ds.Tables[0].Rows[0]["BOI__AdrsGov"]);
                data2BOI.BOI__AdrsPolice = Convert.ToString(ds.Tables[0].Rows[0]["BOI__AdrsPolice"]);
                data2BOI.BOI__AdrsStreet = Convert.ToString(ds.Tables[0].Rows[0]["BOI__AdrsStreet"]);

            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            List<Co_GetCapital2BOI> capital2BOIs = new List<Co_GetCapital2BOI>();
            try
            {
                Co_GetCapital2BOI capital2BOI = new Co_GetCapital2BOI();
                DataSet ds = new DataSet();
                db.openDatabaseConnection();
                string str = "SELECT * FROM[dbo].[Co_GetCapital2BOI]('" + UCR + "')";
                ds = db.executeQuery(str);
                db.closeDatabaseConnection();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    capital2BOI.BOI_CurrncyTypeDesc = Convert.ToString(ds.Tables[0].Rows[i]["BOI_CurrncyTypeDesc"]);
                    capital2BOI.BOI__CapitalTypeDesc = Convert.ToString(ds.Tables[0].Rows[i]["BOI__CapitalTypeDesc"]);
                    capital2BOI.BOI__CapitalValue = Convert.ToString(ds.Tables[0].Rows[i]["BOI__CapitalValue"]);
                    capital2BOI.BOI__UCR = Convert.ToString(ds.Tables[0].Rows[i]["BOI__UCR"]);
                    capital2BOIs.Add(capital2BOI);

                }

                data2BOI.Co_GetCapital2BOI = capital2BOIs;

            }
            catch (Exception ex)
            {
                db.closeDatabaseConnection();
                throw ex;
            }
            return data2BOI;
        }

    }
}
