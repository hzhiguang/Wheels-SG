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
    public class User
    {
        public string nric { get; set; }
        public string pwd { get; set; }
        public string fullname { get; set; }
        public int age { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public int hp { get; set; }
        public bool notification { get; set; }
    }
}
