using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BOR_WS.Modules.Registration
{
    [DataContract]
    public class RegistrationRequest
    {
        [DataMember(IsRequired =true,Order =1)]
        public string FristName { get; set; }
        [DataMember(IsRequired = true,Order =2)]
        public string SecondName { get; set; }
        [DataMember(IsRequired = true,Order =3)]
        public string ThirdName { get; set; }
        [DataMember(IsRequired = true)]
        public string FourthName { get; set; }
        [DataMember(IsRequired = true)]
        public string SurName { get; set; }
        [DataMember(IsRequired = true)]
        public string IDNumber { get; set; }
        [DataMember(IsRequired = true)]
        public string Phone { get; set; }
    }
}