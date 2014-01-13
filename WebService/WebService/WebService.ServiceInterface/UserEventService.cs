using System;
using System.Net;
using System.Collections.Generic;
using WebService.ServiceModel.Types;
using WebService.ServiceModel.Operations;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.Common.Web;

namespace WebService.ServiceInterface
{
    public class UserEventService : Service
    {
        //Get all user event data
        public UserEventResult Get(UserEvents request)
        {
            return new UserEventResult { UserEvent = Db.Select<UserEvent>() };
        }

        //Insert 1 user event object
        public CreateUserEventResult Post(CreateUserEvent request)
        {
            Db.ExecuteSql("INSERT INTO userevent (nric, eventid) VALUES ('" + request.nric + "', '" + request.eventid + "')");
            long id = Db.GetLastInsertId();
            UserEvent e = Db.GetByIdOrDefault<UserEvent>(id);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("User Event does not exist: " + id.ToString()));
            }
            return new CreateUserEventResult { UserEvent = e };
        }
    }
}
