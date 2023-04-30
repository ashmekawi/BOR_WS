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
            companies = db.Database.SqlQuery<Companies>("SELECT * FROM [dbo].[CRRB_GetBOI_ByNID] ('" + GetCompanyInfoNIDRequest.citizenNationalId + "')where left(UCR,1)='1'").ToList();
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
            response.Company = db1.Database.SqlQuery<CompanyData>("SELECT * FROM [dbo].[Fn_getCapitalByUCR] ('" + UCR + "')").FirstOrDefault();
            response.Company.capitals = db1.Database.SqlQuery<Capital>("SELECT * FROM [dbo].[Fn_getCapitalByUCR] ('" + UCR + "')").ToList();
            response.ResponseCode = 200;
            response.ResponseMessage = "Sucsess";
            return response;



        }
        public Co_GetMainData2BOI GetMainData2BOI(string UCR)
        {
            Co_GetMainData2BOI data2BOI = new Co_GetMainData2BOI();
            data2BOI = db1.Database.SqlQuery<Co_GetMainData2BOI>("SELECT * FROM [dbo].[Co_GetMainData2BOI] ('" + UCR + "')").FirstOrDefault();
            List<Co_GetCapital2BOI> capital2BOIs = new List<Co_GetCapital2BOI>();
            capital2BOIs = db1.Database.SqlQuery<Co_GetCapital2BOI>("SELECT * FROM[dbo].[Co_GetCapital2BOI]('" + UCR + "')").ToList();
            data2BOI.Co_GetCapital2BOI = capital2BOIs;
            return data2BOI;
        }
        public GetBOIResponse GetBOI(string UCR)
        {
            GetBOIResponse response = new GetBOIResponse();
            response.BOI = Convert.ToInt32(db.Database.SqlQuery<string>("SELECT [dbo].[GetBOIByUCR] ('" + UCR + "')").FirstOrDefault());
            response.CountOwner = Convert.ToInt32(db.Database.SqlQuery<int>("SELECT [dbo].[GetCountOwner](" + response.BOI + ")").FirstOrDefault());
            return response;
        }
        public List<Arrng> GetArrangements(string citizenNationalId)
        {
            List<Arrng> arrngs = new List<Arrng>();
            arrngs = db.Database.SqlQuery<Arrng>("SELECT * FROM [dbo].[CRRB_GetBOI_ByNID] ('" + citizenNationalId + "') where Left(UCR,1)=2").ToList();
            return arrngs;

        }
        public GetArrangementsDataResponse GetArrangementsData(string UCR)
        {
            GetArrangementsDataResponse response = new GetArrangementsDataResponse();
            response = db.Database.SqlQuery<GetArrangementsDataResponse>("SELECT * FROM [dbo].[CRRB_GetBOI_ByUCR] ('"+UCR+"')").FirstOrDefault() ;
            return response;
        }
        public List<Realbeneficiary> GetRealbeneficiaryByBOIID(int BOIID)
        {
            List<Realbeneficiary> realbeneficiaries = new List<Realbeneficiary>();
            string str = "select " +
                "ID " +
                ",Name0" +
                ",NationDesc" +
                ",CitizenNationalID" +
                ",posdesc" +
                ",MngDesc" +
                ",SngDesc" +

                " from dbo.[BOI_GetRealbeneficiaryByBOIID] (" + BOIID + ")";
            realbeneficiaries = db.Database.SqlQuery<Realbeneficiary>(str).ToList();
            return realbeneficiaries;
        }    
    }
}