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
    public class Location
    {
        public int id { get; set; }
        public string address { get; set; }
        public double x { get; set; }
        public double y { get; set; }
    }
}
