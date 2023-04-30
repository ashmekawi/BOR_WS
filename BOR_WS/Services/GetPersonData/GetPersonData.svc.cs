using BOR_WS.EXTFUN;
using BOR_WS.Modules.GetPersonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BOR_WS.Services.GetPersonData
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetPersonData" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetPersonData.svc or GetPersonData.svc.cs at the Solution Explorer and start debugging.
    public class GetPersonData : IGetPersonData
    {
        CRRB_ServiceContext CRRB = new CRRB_ServiceContext();
        public Person PersonData(int BOIID, int PersonID)
        {
            Person person = new Person();
            person.mainData = CRRB.Database.SqlQuery<PersonMainData>(" select * from CRRB.dbo.Person_GetMainDataByID("+ PersonID +")").FirstOrDefault();
            person.PersonMGM = CRRB.Database.SqlQuery<PerMGM>("select * from  CRRB.dbo.BOI_Person_GetinfoByPersonID("+ BOIID+","+PersonID +")").FirstOrDefault();
            person.identification = CRRB.Database.SqlQuery<Identification>("   select * from  CRRB.dbo.Person_GetIdentificationByPersonID("+PersonID+")").FirstOrDefault();
            person.Address = CRRB.Database.SqlQuery<Address>(" select * from  CRRB.dbo.Person_GetAdrressByPersonID("+ PersonID +")").ToList();
            person.contacts = CRRB.Database.SqlQuery<Contact>(" select * from  CRRB.dbo.Person_GetContactByPersonID(" + PersonID +")").ToList();
            person.RightVoteID = CRRB.Database.SqlQuery<int>(" SELECT isnull([RightVoteID],0) as [RightVoteID]  FROM [CRRB].[dbo].[ShareRealbeneficiary] where BOIID=" + BOIID + " and [PersonID] = " + PersonID + "").FirstOrDefault();
            person.Votingability = CRRB.Database.SqlQuery<int>(" SELECT isnull([Votingability],0) as [RightVoteID]  FROM [CRRB].[dbo].[ShareRealbeneficiary] where BOIID=" + BOIID + " and [PersonID] = " + PersonID + "").FirstOrDefault();
            person.ShareRealbeneficiary = CRRB.Database.SqlQuery<ShareRealbeneficiary>(" select [ID]"+
      ",[BOIID]                 "+
      ",[BOI_TransactionID]     "+
      ",[PersonID]              "+
      ",[ShareTypeID]           "+
      ",[OwnershipPercentage]   "+
      ",[NumbeofShares]         "+
      ",[StockValue]            "+
      //  ",CONVERT(CHAR(8),[OwnershipDate],112) as [OwnershipDate]         " +
      ",CONVERT (datetime,convert(char(8),OwnershipDate ))  as [OwnershipDate]       " +
      ",[RightVoteID]           "+
      ",[Votingability]         "+
      ",[RecStatus]             "+
      ",[UserID]                "+
      ",[RecTimeStamp]          "+
      ",[TrnsDatetime]          "+
      ",[ElemntNo]              "+
      ",[RequestID]  FROM [CRRB].[dbo].[ShareRealbeneficiary] where BOIID="+BOIID+ " and [PersonID] = "+PersonID+"").ToList();

            return person;
        }
    }
}
