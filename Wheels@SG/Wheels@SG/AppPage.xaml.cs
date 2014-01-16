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
            if (settings.Contains("userFlag"))
            {
                User user = (User)settings["userFlag"];
            }
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
                GraphicsLayer graphicsLayer = esriMap.Layers["MyGraphicsLayer"] as GraphicsLayer;
                Graphic graphic = new ESRI.ArcGIS.Client.Graphic()
                {
                    Geometry = map,
                    Symbol = LayoutRoot.Resources["DefaultMarkerSymbol"] as ESRI.ArcGIS.Client.Symbols.Symbol,
                };
                graphic.SetZIndex(1);
                graphicsLayer.Graphics.Add(graphic);
            });
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

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            esriMap.Zoom(2);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            esriMap.Zoom(0.5);
        }
    }
}