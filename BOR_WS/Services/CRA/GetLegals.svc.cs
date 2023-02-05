using BOR_WS.DataBase;
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
        public GetCompanyInfoNIDResponse GetCompanyInfoNIDResponse(GetCompanyInfoNIDRequest GetCompanyInfoNIDRequest)
        {
            
           
                GetCompanyInfoNIDResponse response = new GetCompanyInfoNIDResponse();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = "select * from dbo.fn_ps_get_comp_info_pid(@prsn_id)";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.Add(new SqlParameter("@prsn_id", GetCompanyInfoNIDRequest.citizenNationalId));
                    response.Companies = GetCompaniesInformation(sqlCommand);
                    return response;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
           
        }
        public List<Companies> GetCompaniesInformation(SqlCommand sqlCommand)
        {
            CRADB dbManager = new CRADB();
            dbManager.openDatabaseConnection();
            try
            {
                List<Companies> companiesInformation = new List<Companies>();
                DataSet dbSet = new DataSet();
                sqlCommand.Connection = dbManager.databaseConnection;
                sqlCommand.CommandTimeout = 0;
                dbSet = dbManager.executeSqlCommand(sqlCommand);

                if (dbSet.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dbSet.Tables[0].Rows.Count; i++)
                    {
                        Companies companyInformation = new Companies();
                        DataRow dbRow = dbSet.Tables[0].Rows[i];
                        companyInformation.UCR = dbRow["ucr"].ToString();
                        companyInformation.CRA = Convert.ToInt32(dbRow["CRA_NUM"].ToString());
                        companyInformation.RegNum = Convert.ToInt32(dbRow["REGISTRATION_NO"].ToString());
                        companyInformation.OfficeName = dbRow["OFFICE_NAME"].ToString();
                        companyInformation.Governorate = dbRow["Gov_Name"].ToString();
                        companyInformation.uid = dbRow["TAX_NUMBER"].ToString();
                        companyInformation.companyName = dbRow["COMP_NAME"].ToString();
                        companyInformation.position = dbRow["PERSON_POSITION"].ToString();
                        companyInformation.NextRenewalDate = dbRow["NEXT_RENEW"].ToString();
                        companyInformation.RegCreationDate = dbRow["REG_DATE"].ToString();
                        companyInformation.signStatus = Convert.ToInt32(dbRow["RNW_VLD_PRSN_FLG"].ToString());
                        companyInformation.companySecurityApprovalFlag = Convert.ToInt32(dbRow["RNW_VLD_SEC_FLG"].ToString());
                        companyInformation.companystatusFlag = Convert.ToInt32(dbRow["RNW_VLD_COMP_STATS_FLG"].ToString());
                        companyInformation.ClassCode = Convert.ToInt32(dbRow["FK_CLASSCODE"].ToString());

                        companiesInformation.Add(companyInformation);
                    }
                    dbManager.closeDatabaseConnection();
                    return companiesInformation;
                }
                else
                {
                    dbManager.closeDatabaseConnection();
                    throw new DBException(404, "لا يوجد منشئات لهذا الرقم القومي");
                }
            }
            catch (Exception ex)
            {
                dbManager.closeDatabaseConnection();
                throw ex;
            }

        }

        public int GetBOI(string UCR)
        {
            return 5;
        }

    }
}
