using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using static GibbonVk.MainPage;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Windows.UI.Xaml.Media.Imaging;
using GibbonVk.Models;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GibbonVk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {


        public UserPage()
        {
            this.InitializeComponent();
            GetUserInfo();
            GetUserPhotos();
        }

        private async Task GetUserPhotos()
        {

            List<PhotoModel> listPhotoModels = new List<PhotoModel>();

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri("https://api.vk.com/method/photos.getAll?owner_id=" 
                + _UserID +
                "&extended=0" +
                "&count=20" +
                "&access_token=" +
                _Token + __VKAPI);

            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string resultResponse = await httpResponse.Content.ReadAsStringAsync();
            JObject photos = JObject.Parse(resultResponse);
            
            int photosCount = photos["response"]["items"].Count();
            for(int i = 0; i < photosCount; i++)
            {
                int photoId = Int32.Parse(photos["response"]["items"][i]["id"].ToString());
                int sizes = photos["response"]["items"][i]["sizes"].Count();
                string imageUrl = photos["response"]["items"][i]["sizes"][sizes - 1]["url"].ToString();
                listPhotoModels.Add(new PhotoModel(photoId, imageUrl));
            }

            photosLine.ItemsSource = listPhotoModels;


            //Dictionary<string, string> responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultResponse);

        }

        private async Task GetUserInfo()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri("https://api.vk.com/method/users.get?user_id=" + _UserID + "&fields=photo_200,online,is_friend,status,bdate&access_token=" + _Token + __VKAPI);

            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string resultResponse = await httpResponse.Content.ReadAsStringAsync();

            resultResponse = resultResponse.Substring(13, resultResponse.Length - 15);
            Dictionary<string, string> responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultResponse);

            BitmapImage bitmap = new BitmapImage();
            bitmap.UriSource = new Uri(responseJson["photo_200"]);
            profileMainImage.Source = bitmap;
            profileFLName.Text = responseJson["first_name"] + " " + responseJson["last_name"];
            profileStatus.Text = responseJson["status"];
            if (responseJson["is_friend"] == "1")
            {
                addFriendButton.Content = "Remove Friend";
                addFriendButton.IsEnabled = false;
            }
            if (responseJson["online"] == "0")
            {
                profileOnlineStatus.Text = "Не в сети";
            }
            else
            {
                profileOnlineStatus.Text = "В сети";
            }

        }
    }
}
