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
    public class Event
    {
        public int id { get; set; }
        public string eventname { get; set; }
        public string description { get; set; }
        public int locationid { get; set; }
    }
}
