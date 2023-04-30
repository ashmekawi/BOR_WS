using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BOR_WS.Modules.GetPersonData
{
    public class Person
    {
        public PersonMainData mainData { get; set; }
        public PerMGM PersonMGM { get; set; }
        public Identification identification { get; set; }
        public List<Address> Address { get; set; }
        public List<Contact> contacts { get; set; }
        public List<ShareRealbeneficiary> ShareRealbeneficiary { get; set; }
        public int RightVoteID { get; set; }
        public int Votingability { get; set; }

    }
}