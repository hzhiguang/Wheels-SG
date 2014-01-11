using System.Runtime.Serialization;

namespace WebService.ServiceModel.Types
{
    [DataContract]
    public class UserEvent
    {
        [DataMember]
        public string nric { get; set; }

        [DataMember]
        public int eventid { get; set; }
    }
}