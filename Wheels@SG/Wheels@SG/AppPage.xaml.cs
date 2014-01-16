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
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.Geometry;

namespace Wheels_SG
{
    public partial class AppPage : PhoneApplicationPage
    {
        public AppPage()
        {
            InitializeComponent();
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