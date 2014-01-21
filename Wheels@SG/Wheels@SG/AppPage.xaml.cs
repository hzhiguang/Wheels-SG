using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Wheels_SG.Model;
using System.IO.IsolatedStorage;
using System.Threading;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Tasks;
using ESRI.ArcGIS.Client.Symbols;
using System.Windows.Media.Imaging;

namespace Wheels_SG
{
    public partial class AppPage : PhoneApplicationPage
    {
        private GeoCoordinateWatcher geoWatcher;
        private LatLng currentLocation;
        private bool isNear = false;

        private static WheelsApi api = new WheelsApi();
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public AppPage()
        {
            InitializeComponent();
            //if (settings.Contains("userFlag"))
            //{
            //    User user = (User)settings["userFlag"];
            //}
            InitWatcher();
        }

        private void InitWatcher()
        {
            geoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            geoWatcher.MovementThreshold = 10;
            geoWatcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(geoWatcher_StatusChanged);
            geoWatcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(geoWatcher_PositionChanged);
            new Thread(backgroundLocationService).Start();
        }

        void geoWatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            currentLocation = new LatLng(e.Position.Location.Latitude, e.Position.Location.Longitude);
            LatLng input = new LatLng(e.Position.Location.Latitude, e.Position.Location.Longitude);
            MapPoint map = new MapPoint();
            api.convertLatLng(input, mappoint =>
            {
                map = mappoint;
                GraphicsLayer graphicsLayer = esriMap.Layers["BufferLayer"] as GraphicsLayer;
                drawBufferCircle(1000, 40, map, graphicsLayer);
            });

            api.RetrieveNearbyEvents(e.Position.Location.Latitude, e.Position.Location.Longitude, 0.1, (List<Event> events) =>
            {
                for (int i = 0; i < events.Count - 1; i++)
                {
                    api.RetrieveLocations((List<Model.Location> locs) =>
                    {
                        GraphicsLayer gl = esriMap.Layers["PinLayout"] as GraphicsLayer;
                        for (int a = 0; a < locs.Count - 1; a++)
                        {
                            if (locs.ElementAt(a).id == events.ElementAt(i).locationid)
                            {
                                LatLng pin = new LatLng(locs.ElementAt(a).x, locs.ElementAt(a).y);
                                MapPoint mp = new MapPoint();
                                api.convertLatLng(pin, mappoint =>
                                {
                                    mp = mappoint;
                                    PictureMarkerSymbol ps = new PictureMarkerSymbol();
                                    ps.Source = new BitmapImage(new Uri("Picture/pin.png", UriKind.Relative));
                                    Graphic graphic = new Graphic()
                                    {
                                        Geometry = mp,
                                        Symbol = ps,
                                    };
                                    gl.Graphics.Add(graphic);
                                });
                            }
                        }
                    });
                }
            });
        }

        public void drawBufferCircle(double radius, int pointCount, MapPoint currentPoint, GraphicsLayer gl)
        {
            MapPoint point = currentPoint;
            var pl = new ESRI.ArcGIS.Client.Geometry.Polyline();
            var polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
            var routePoint = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            for (int i = 1; i <= pointCount; i++)
            {
                double x;
                double y;
                x = (point.X + radius * Math.Cos(2 * Math.PI / pointCount * i));
                y = (point.Y + radius * Math.Sin(2 * Math.PI / pointCount * i));
                routePoint.Add(new MapPoint(x, y));
            }
            routePoint.Add(routePoint[0]);
            polygon.Rings.Add(routePoint);
            GraphicsLayer mygraphicslayer = gl;
            mygraphicslayer.ClearGraphics();
            Graphic graphic = new Graphic()
            {
                Geometry = polygon,
                Symbol = LayoutRoot.Resources["DefaultBufferSymbol"] as ESRI.ArcGIS.Client.Symbols.Symbol,
            };
            mygraphicslayer.Graphics.Add(graphic);
        }

        void geoWatcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    if (geoWatcher.Permission == GeoPositionPermission.Denied)
                    {
                        MessageBox.Show("You have disabled Location Service.");
                    }
                    else
                    {
                        MessageBox.Show("Location Service is not functioning on this device.");
                    }
                    break;
                case GeoPositionStatus.Initializing:
                    break;
                case GeoPositionStatus.NoData:
                    break;
                case GeoPositionStatus.Ready:
                    break;
            }
        }

        void backgroundLocationService()
        {
            geoWatcher.TryStart(true, TimeSpan.FromSeconds(1));
        }

        void GeometryService_AutoCompleteCompleted(object sender, GraphicsEventArgs args)
        {
            GraphicsLayer graphicsLayer = esriMap.Layers["MyGraphicsLayer"] as GraphicsLayer;
            foreach (Graphic graphic in args.Results)
            {
                graphic.Symbol = LayoutRoot.Resources["DefaultBufferSymbol"] as ESRI.ArcGIS.Client.Symbols.Symbol;
                graphicsLayer.Graphics.Add(graphic);
            }
        }

        private void GraphicsLayer_MouseLeftButtonDown(object sender, ESRI.ArcGIS.Client.GraphicMouseButtonEventArgs e)
        {
            //LatLng input = currentLocation;
            //MapPoint map = new MapPoint();
            //api.convertLatLng(input, mappoint =>
            //{
            //    map = mappoint;
            //    MapPoint click = esriMap.ScreenToMap(e.GetPosition(esriMap));
            //    double x = click.X;
            //    double minX = x - 300;
            //    double maxX = x + 300;
            //    double y = click.Y;
            //    if ((map.X < maxX) && (map.X > minX))
            //    {
            //        MyInfoWindow.Anchor = map;
            //        //MyInfoWindow.Content = showtime.theater.name;
            //        MyInfoWindow.IsOpen = true;
            //    }
            //});
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            esriMap.Zoom(2);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            esriMap.Zoom(0.5);
        }

        private void event_Click(object sender, EventArgs e)
        {
            createEvent.IsOpen = true;
            createEvent.Visibility = Visibility.Visible;
            createEvent.VerticalOffset = -550;
        }

        private void event_Opened(object sender, EventArgs e)
        {
            //if (isNear == false)
            //{
            //    createEvent.IsOpen = false;
            //}
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            Event eve = new Event();
            eve.eventname = tbEventName.Text.ToString();
            eve.description = tbDescription.Text.ToString();
            Wheels_SG.Model.Location loc = new Wheels_SG.Model.Location();
            loc.address = tbAddress.Text.ToString();
            loc.x = currentLocation.lat;
            loc.y = currentLocation.lng;
            api.CreateLocation(loc, (List<Wheels_SG.Model.Location> locs) =>
            {

            });
            api.RetrieveLocations((List<Model.Location> locations) =>
            {
                eve.locationid = locations.ElementAt(locations.Count - 1).id;
                api.CreateEvent(eve, (List<Event> eves) =>
                {
                    MessageBox.Show("Event Successfully Created");
                    createEvent.IsOpen = false;
                });
            });
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            createEvent.IsOpen = false;
        }
    }
}