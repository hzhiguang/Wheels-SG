using System.Runtime.Serialization;

namespace WebService.ServiceModel.Types
{
    [DataContract]
    public class Location
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public double x { get; set; }

        [DataMember]
        public double y { get; set; }
    }
}