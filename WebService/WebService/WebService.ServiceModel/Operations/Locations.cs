using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using WebService.ServiceModel.Types;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace WebService.ServiceModel.Operations
{
    [DataContract]
    [Route("/json/location")]
    public class Locations
    {
    }

    [DataContract]
    [Route("/json/createLocation", "GET")]
    public class CreateLocation
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public double x { get; set; }
        [DataMember]
        public double y { get; set; }

        public CreateLocation(string address, double x, double y)
        {
            this.address = address;
            this.x = x;
            this.y = y;
        }
    }

    [DataContract]
    [Route("/json/location/{id}")]
    public class GetLocationDetails
    {
        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    [Route("/json/updateLocation", "POST")]
    public class UpdateLocation
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public double x { get; set; }
        [DataMember]
        public double y { get; set; }

        public UpdateLocation(string address, double x, double y)
        {
            this.address = address;
            this.x = x;
            this.y = y;
        }
    }

    [DataContract]
    public class LocationResult : IHasResponseStatus
    {
        public LocationResult()
        {
            this.Location = new List<Location>();
        }

        [DataMember]
        public List<Location> Location { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class CreateLocationResult : IHasResponseStatus
    {
        public CreateLocationResult()
        {
            this.Location = Location;
        }

        [DataMember]
        public Location Location { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class LocationDetailsResult : IHasResponseStatus
    {
        public LocationDetailsResult()
        {
            this.Location = Location;
        }

        [DataMember]
        public Location Location { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class UpdateLocationResult : IHasResponseStatus
    {
        public UpdateLocationResult()
        {
            this.Location = Location;
        }

        [DataMember]
        public Location Location { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}
