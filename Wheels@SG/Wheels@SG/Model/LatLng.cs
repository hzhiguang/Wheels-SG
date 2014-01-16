using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wheels_SG.Model
{
    public class LatLng
    {
        public double lng { get; set; }

        public double lat { get; set; }

        public LatLng(double lat, double lng)
        {
            this.lng = lng;
            this.lat = lat;
        }
    }
}
