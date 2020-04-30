using GibbonVk.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GibbonVk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public const string __VKAPI = "&v=5.103";
        public const string __APPID = "6044506";
        public const string __VKAPIURL = "https://api.vk.com/method/";
        public static string _Token, _UserID;
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AuthWebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.Uri.ToString().IndexOf("access_token=") != -1)
            {
                GetUserToken(e.Uri.ToString());
            }
            else if (e.Uri.ToString() == "https://oauth.vk.com/error?err=2")
            {
                AuthWebView.Navigate(new Uri(""));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!localSettingSwitch.IsOn)
            {
                WebPanel.Visibility = Visibility.Visible;
                Uri uriVKAuth = new Uri("https://oauth.vk.com/authorize?client_id=6044506&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=friends,messages,photos&response_type=token" + __VKAPI);
                AuthWebView.Navigate(uriVKAuth);
            }
            else
            {
                if (localSettings.Values["main_token"] != null)
                {
                    _Token = localSettings.Values["main_token"].ToString();
                    _UserID = localSettings.Values["main_user_id"].ToString();
                    NavView.IsPaneToggleButtonVisible = true;
                    Content.Navigate(typeof(MessagesPage), null, new DrillInNavigationTransitionInfo());
                }
            }

        }

        private void GetUserToken(String uri)
        {
            char[] Symbols = { '=', '&' };
            string[] URL = uri.Split(Symbols);
            AuthWebView.Visibility = Visibility.Collapsed;
            _Token = URL[1];
            _UserID = URL[5];

            NavView.IsPaneToggleButtonVisible = true;
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["main_token"] = _Token;
            localSettings.Values["main_user_id"] = _UserID;
            //_UserID = "176117354";

            Content.Navigate(typeof(MessagesPage), null, new DrillInNavigationTransitionInfo());

        }
    }
}
