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
    [Route("/json/event")]
    public class Events
    {
    }

    [DataContract]
    [Route("/json/createEvent", "POST")]
    public class CreateEvent
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string eventname { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int locationid { get; set; }

        public CreateEvent(string eventname, string description, int locationid)
        {
            this.eventname = eventname;
            this.description = description;
            this.locationid = locationid;
        }
    }

    [DataContract]
    [Route("/json/event/{id}")]
    public class GetAnalysisDetails
    {
        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    public class EventResult : IHasResponseStatus
    {
        public EventResult()
        {
            this.Event = new List<Event>();
        }

        [DataMember]
        public List<Event> Event { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class CreateEventResult : IHasResponseStatus
    {
        public CreateEventResult()
        {
            this.Event = Event;
        }

        [DataMember]
        public Event Event { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class EventDetailsResult : IHasResponseStatus
    {
        public EventDetailsResult()
        {
            this.Event = Event;
        }

        [DataMember]
        public Event Event { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}