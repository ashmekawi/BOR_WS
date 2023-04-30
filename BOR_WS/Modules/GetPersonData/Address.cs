using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class Address
    {

            public int ID { get; set; }
            public int PersonID                  {get;set;}
            public int NationID                  {get;set;}
            public int GovernorateID             {get;set;}
            public int PoliceStationID           {get;set;}
            public int VillageID                 {get;set;}
            public int StreetID                  {get;set;}
            public int PersonAdrsTypeID          {get;set;}
            public string AdrsTypeDesc              {get;set;}
            public string  NationName                {get;set;}
            public string  GovName                   {get;set;}
            public string  PoliceName                {get;set;}
            public string  VillageName               {get;set;}
            public string  StreetName                {get;set;}
            public string  Adrs                      {get;set;}
    }
}