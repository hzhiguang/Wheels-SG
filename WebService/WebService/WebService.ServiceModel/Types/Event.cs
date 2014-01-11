using System.Runtime.Serialization;

namespace WebService.ServiceModel.Types
{
    [DataContract]
    public class Event
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string eventname { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int locationid { get; set; }
    }
}
