using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class ShareRealbeneficiary
    {
      public decimal  ID { get; set; }
      public decimal BOIID { get; set; }
      public decimal BOI_TransactionID { get; set; }
      public decimal PersonID { get; set; }
      public int ShareTypeID { get; set; }
      public decimal OwnershipPercentage { get; set; }
      public string NumbeofShares { get; set; }
      public decimal StockValue { get; set; }
      public DateTime OwnershipDate { get; set; }
    
     
      public int  UserID { get; set; }
      public DateTime RecTimeStamp { get; set; }
      public decimal TrnsDatetime { get; set; }
      public int ElemntNo { get; set; }
      public decimal RequestID { get; set; }
    }
}