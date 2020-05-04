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
using static GibbonVk.MainPage;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using GibbonVk.Models;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GibbonVk.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {
        ConversationsModel conversationsModel;
        ObservableCollection<HistoryModel> historyModels = new ObservableCollection<HistoryModel>();
        public HistoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            conversationsModel = e.Parameter as ConversationsModel;
            chatUserName.Text = conversationsModel.FullName;
            _ = GetHistory(conversationsModel.PeerId);
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;
                var text = (sender as TextBox).Text;
            }
        }

        private async Task GetHistory(int user_id)
        {
            historyModels.Clear();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri("https://api.vk.com/method/messages.getHistory?count=30" +
                "&user_id=" + user_id +
                "&access_token=" +
                _Token + __VKAPI);

            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string resultResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HistoryWrapper>(resultResponse);
            foreach (var item in result.response.items)
            {
                HistoryModel historyModel = new HistoryModel();
                historyModel.date = item.date;
                historyModel.from_id = item.from_id;
                historyModel.text = item.text;
                historyModel.replyMessage = item.reply_message;
                if (item.from_id.ToString() == _UserID)
                {
                    historyModel.isSelf = true;
                    historyModel.imageUrl = "https://sun9-28.userapi.com/impg/c858132/v858132220/1db0dd/Z1zeI1bjQ_I.jpg?size=200x0&quality=90&sign=aec2bd4ee207db01b7b99a4036808533";
                }
                else
                {
                    historyModel.isSelf = false;
                    historyModel.imageUrl = conversationsModel.Photo100;
                }
                if (item.attachments.Count != 0)
                {
                    historyModel.attachmentsHistories = item.attachments;
                }
                historyModels.Add(historyModel);
            }
            listHistory.ItemsSource = historyModels.Reverse();
        }
    }
}
