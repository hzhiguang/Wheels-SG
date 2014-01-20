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
    public class EventService : Service
    {
        //Get all event data
        public EventResult Get(Events request)
        {
            return new EventResult { Event = Db.Select<Event>() };
        }

        //Insert 1 event object
        public CreateEventResult GET(CreateEvent request)
        {
            Db.ExecuteSql("INSERT INTO event (eventname, description, locationid) Values ('" + request.eventname + "','" + request.description + "','" + request.locationid + "')");
            long id = Db.GetLastInsertId();
            Event e = Db.GetByIdOrDefault<Event>(id);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("Event does not exist: " + id.ToString()));
            }

            return new CreateEventResult { Event = e };
        }

        //Retrieve 1 event object by id
        public EventDetailsResult Get(GetEventDetails request)
        {
            var eve = Db.GetByIdOrDefault<Event>(request.id);
            if (eve == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("Event does not exist: " + request.id));
            }
            return new EventDetailsResult { Event = eve };
        }

        //Retrieve events near
        public EventNearResult Get(GetEventNear request)
        {
            return new EventNearResult { Event = Db.Query<Event>("SELECT * FROM event INNER JOIN location ON event.locationid = location.id WHERE ST_DWithin(geom, ST_GeomFromText('POINT(" + request.x + " " + request.y + ")', 3414), " + request.radius + ")") };
        }

        //Update 1 event object
        public UpdateEventResult Post(UpdateEvent request)
        {
            Db.ExecuteSql("UPDATE event SET eventname='" + request.eventname + "', description='" + request.description + "', locationid='" + request.locationid + "' WHERE id='" + request.id + "'");
            long id = Db.GetLastInsertId();
            Event e = Db.GetByIdOrDefault<Event>(id);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("Event does not exist: " + id.ToString()));
            }
            return new UpdateEventResult { Event = e };
        }
    }
}