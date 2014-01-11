using System.Runtime.Serialization;
using System;

namespace WebService.ServiceModel.Types
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string nric { get; set; }

        [DataMember]
        public string pwd { get; set; }

        [DataMember]
        public string fullname { get; set; }

        [DataMember]
        public int age { get; set; }

        [DataMember]
        public DateTime dob { get; set; }

        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public int hp { get; set; }

        [DataMember]
        public bool notification { get; set; }
    }
}