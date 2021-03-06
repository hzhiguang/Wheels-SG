﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using RestSharp;
using RestSharp.Deserializers;
using ESRI.ArcGIS.Client.Geometry;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Wheels_SG.Model
{
    public class WheelsApi
    {
        public const string BaseUrl = "http://localhost:54829";

        public WheelsApi() { }

        public RestRequestAsyncHandle ExecuteAync<T>(RestRequest request, Action<T> callback) where T : new()
        {
            var client = new RestClient(BaseUrl);
            //client.BaseUrl = BaseUrl;
            request.RequestFormat = DataFormat.Json;
            request.RootElement = "content";
            return client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    string message = "Error retrieving response. Exception: " + response.ErrorException + " Message: " + response.ErrorMessage;
                    Console.WriteLine(message);
                }
                callback(response.Data);
            });
        }

        public RestRequestAsyncHandle Execute(RestRequest request, Action<IRestResponse> callback)
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            request.RequestFormat = DataFormat.Json;
            request.RootElement = "content";
            return client.ExecuteAsync(request, (IRestResponse response) =>
            {
                callback(response);
            });
        }

        //public RestRequestAsyncHandle LoadFromLink<T>(String selfRel, Link link, Action<T> callback)
        //{
        //    var request = new RestRequest(Method.GET);
        //    request.RootElement = "content";
        //    request.Resource = sanitizeFaultyUrl(link.href, selfRel);
        //    return Execute(request, (IRestResponse data) => { callback(JsonConvert.DeserializeObject<T>(JObject.Parse(data.Content)["content"].ToString())); });
        //}

        public RestRequestAsyncHandle Login(string username, string password, Action<List<User>> callback)
        {
            var request = new RestRequest("api/json/user", Method.GET);
            //request.Resource = "user";
            //request.AddParameter("nric", username);
            //request.AddParameter("pwd", password);
            return ExecuteAync<List<User>>(request, callback);
        }

        public RestRequestAsyncHandle CreateLocation(Location loc, Action<List<Location>> callback)
        {
            var request = new RestRequest("api/json/createLocation", Method.GET);
            request.AddParameter("address", loc.address);
            request.AddParameter("x", loc.x);
            request.AddParameter("y", loc.y);
            return ExecuteAync<List<Location>>(request, callback);
        }

        public RestRequestAsyncHandle RetrieveLocations(Action<List<Location>> callback)
        {
            var request = new RestRequest("api/json/location", Method.GET);
            return Execute(request, (IRestResponse data) => { callback(JsonConvert.DeserializeObject<List<Location>>(JObject.Parse(data.Content)["Location"].ToString())); });
        }

        public RestRequestAsyncHandle CreateEvent(Event eve, Action<List<Event>> callback)
        {
            var request = new RestRequest("api/json/createEvent", Method.GET);
            request.AddParameter("eventname", eve.eventname);
            request.AddParameter("description", eve.description);
            request.AddParameter("locationid", eve.locationid);
            return ExecuteAync<List<Event>>(request, callback);
        }

        public RestRequestAsyncHandle RetrieveNearbyEvents(double x, double y, double rad, Action<List<Event>> callback)
        {
            var request = new RestRequest("api/json/event/near", Method.GET);
            request.AddParameter("x", x);
            request.AddParameter("y", y);
            request.AddParameter("radius", rad);
            return Execute(request, (IRestResponse data) => { callback(JsonConvert.DeserializeObject<List<Event>>(JObject.Parse(data.Content)["Event"].ToString())); });
        }

        public RestRequestAsyncHandle convertLatLng(LatLng latlng, Action<MapPoint> callback)
        {
            RestClient client = new RestClient();
            client.BaseUrl = "http://tasks.arcgisonline.com";
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.Resource = String.Format("ArcGIS/rest/services/Geometry/GeometryServer/project?inSR=4326&outSR=3414&geometries={0}%2C{1}&f=pjson", latlng.lng, latlng.lat);
            return client.ExecuteAsync(request, response => { JToken coords = JObject.Parse(response.Content)["geometries"].First; callback(new MapPoint((Convert.ToDouble(coords.SelectToken("x").ToString())), Convert.ToDouble(coords.SelectToken("y").ToString()))); });
        }
    }
}