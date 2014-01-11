﻿using System;
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
        public CreateEventResult Post(CreateEvent request)
        {
            Db.ExecuteSql("INSERT INTO event (id, eventname, description, locationid) VALUES ('" + request.id + "', '" + request.eventname + "', '" + request.description + "', '" + request.description + "', '" + request.locationid + "')");
            long id = Db.GetLastInsertId();
            Event e = Db.GetByIdOrDefault<Event>(id);
            if (e == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("Event does not exist: " + id.ToString()));
            }
            return new CreateEventResult { Event = e };
        }

        //Retrieve 1 event object by id
        public EventDetailsResult Get(GetAnalysisDetails request)
        {
            var eve = Db.GetByIdOrDefault<Event>(request.id);
            if (eve == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, new ArgumentException("Event does not exist: " + request.id));
            }
            return new EventDetailsResult { Event = eve };
        }
    }
}