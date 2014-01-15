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
using Wheels_SG.Model;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;

namespace Wheels_SG
{
    public partial class Login : PhoneApplicationPage
    {
        private static WheelsApi api = new WheelsApi();
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            api.Login(tbUser.Text.ToString(), pbPwd.Password.ToString(), (List<User> users) => { saveUser(users.ElementAt(0)); });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (settings.Contains("userFlag"))
            {
                NavigationService.Navigate(new Uri("/AppPage.xaml", UriKind.Relative));
            }
        }

        private void saveUser(User user)
        {
            settings.Add("userFlag", user);
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}