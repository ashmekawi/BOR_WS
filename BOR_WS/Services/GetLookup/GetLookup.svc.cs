using BOR_WS.DataBase;
using BOR_WS.EXTFUN;
using BOR_WS.Modules.GetLookup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.GetLookup
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetLookup" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetLookup.svc or GetLookup.svc.cs at the Solution Explorer and start debugging.
    public class GetLookup : IGetLookup
    {
        CRRB_ServiceContext servicedb = new CRRB_ServiceContext();
        //SELECT * FROM [dbo].[CRRB_GetLockup] ('activities',0)
        public GetLookupResponse GetLookupResponse(GetLookupRequest request)
        {
            GetLookupResponse response = new GetLookupResponse();
            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "SELECT * FROM [dbo].[CRRB_GetLockup] (@TblName,@CondValue)";
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add(new SqlParameter("@TblName", request.TblName));
                sqlCommand.Parameters.Add(new SqlParameter("@CondValue", request.CondValue));
                response.Lookup = GetLookups(sqlCommand);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public List<Lookup> GetLookups(SqlCommand sqlCommand)
        {
            DBMan dbManager = new DBMan();
            dbManager.openDatabaseConnection();
            try
            {
                List<Lookup> Lookups = new List<Lookup>();
                DataSet dbSet = new DataSet();
                sqlCommand.Connection = dbManager.databaseConnection;
                sqlCommand.CommandTimeout = 0;
                dbSet = dbManager.executeSqlCommand(sqlCommand);

                if (dbSet.Tables[0].Rows.Count > 0)
                {

                    for (int i = 0; i < dbSet.Tables[0].Rows.Count; i++)
                    {
                        Lookup Lookup = new Lookup();
                        DataRow dbRow = dbSet.Tables[0].Rows[i];
                        Lookup.id = Convert.ToInt32(dbRow["id"].ToString());
                        Lookup.adesc = dbRow["adesc"].ToString();
                        Lookups.Add(Lookup);
                    }
                    dbManager.closeDatabaseConnection();
                    return Lookups;
                }
                else
                {
                    dbManager.closeDatabaseConnection();
                    throw new DBException(404, "لا يوجد جدول بهذا الاسم");
                }
            }
            catch (Exception ex)
            {
                dbManager.closeDatabaseConnection();
                throw ex;
            }

        }

        public AddOwnerLookUps AddOwnerLookUp()
        {
            AddOwnerLookUps AddOwnerLookUps = new AddOwnerLookUps();
            AddOwnerLookUps.NATIONALITY = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('NATIONALITY' ,0)").ToList();
            AddOwnerLookUps.PerIDType = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('PerIDType' ,0)").ToList();
            AddOwnerLookUps.RightVote = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('RightVote' ,0)").ToList();
            AddOwnerLookUps.Contactype = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('Contactype', 0)").ToList();
            AddOwnerLookUps.Bank = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('Bank', 0)").ToList();
            AddOwnerLookUps.DOCType = servicedb.Database.SqlQuery<Lookup>("SELECT [ID]      ,[Adesc]    FROM [CRRB_Service].[dbo].[DocType]").ToList();
            AddOwnerLookUps.ShareType = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('ShareType', 0)").ToList();
            AddOwnerLookUps.PersonAdrsType = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('PersonAdrsType', 0)").ToList();
            AddOwnerLookUps.Stockmarket = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('Stockmarket', 0)").ToList();
            return AddOwnerLookUps;
        }
        public AddArrLookUps AddArrLookUp()
        {
            AddArrLookUps AddArrLookUps = new AddArrLookUps();
            AddArrLookUps.IssueLaw = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('IssueLaw' ,2)").ToList();
            AddArrLookUps.ArrType = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('Legal' ,2)").ToList();
            AddArrLookUps.Permission = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM [dbo].[CRRB_GetLockup] ('PermissionSource' ,0)").ToList();
            AddArrLookUps.Contact = servicedb.Database.SqlQuery<Lookup>("SELECT * FROM[dbo].[CRRB_GetLockup]('Contactype', 0)").ToList();
            return AddArrLookUps;
        }
        public List<Activity> Activities_Finder(string Search)
        {
            List<Activity> activities = new List<Activity>();
            activities = servicedb.Database.SqlQuery<Activity>("SELECT * FROM [dbo].[Activities_Finder] ('"+Search+"')").ToList();
            return activities;

        }


    }
}
