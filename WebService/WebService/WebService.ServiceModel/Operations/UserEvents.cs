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
    [Route("/json/userEvent")]
    public class UserEvents
    {
    }

    [DataContract]
    [Route("/json/createUserEvent", "POST")]
    public class CreateUserEvent
    {
        [DataMember]
        public string nric { get; set; }
        [DataMember]
        public int eventid { get; set; }

        public CreateUserEvent(string nric, int eventid)
        {
            this.nric = nric;
            this.eventid = eventid;
        }
    }

    [DataContract]
    public class UserEventResult : IHasResponseStatus
    {
        public UserEventResult()
        {
            this.UserEvent = new List<UserEvent>();
        }

        [DataMember]
        public List<UserEvent> UserEvent { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }

    [DataContract]
    public class CreateUserEventResult : IHasResponseStatus
    {
        public CreateUserEventResult()
        {
            this.UserEvent = UserEvent;
        }

        [DataMember]
        public UserEvent UserEvent { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }
    }
}
