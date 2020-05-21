using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using static GibbonVk.MainPage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using GibbonVk.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Imaging;
using System.Numerics;
using Windows.Media.Core;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GibbonVk.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagesPage : Page
    {
        public ObservableCollection<LongPollAnswer> LongPollServerAnswer { get; set; }
        public ObservableCollection<LongPollServerResponse> LongPollServerInfo { get; set; }
        ObservableCollection<ConversationsModel> conversationsModels = new ObservableCollection<ConversationsModel>();

        public ConversationsModel friendList;

        public MessagesPage()
        {
            this.InitializeComponent();
            LongPollServerInfo = new ObservableCollection<LongPollServerResponse>();
            LongPollServerAnswer = new ObservableCollection<LongPollAnswer>();
            _ = GetConversations();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            UpdateTitleBarLayout(coreTitleBar);
            Window.Current.SetTitleBar(AppTitleBar);
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            UpdateTitleBarLayout(sender);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (sender.IsVisible)
            {
                AppTitleBar.Visibility = Visibility.Visible;
            }
            else
            {
                AppTitleBar.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateTitleBarLayout(CoreApplicationViewTitleBar coreTitleBar)
        {
            // Update title bar control size as needed to account for system size changes.
            Separator.Height = coreTitleBar.Height;
            AppTitleBar.Height = coreTitleBar.Height;
        }

        private async Task RunLongPollRequests()
        {
            for (; ; )
            {
                LongPollServerInfo.Clear();
                LongPollServerAnswer.Clear();
                await MessangerFacade.PopulateLongPollServerInfoAsync(LongPollServerInfo);
                await MessangerFacade.PopulateLongPollAnswersAsync(LongPollServerAnswer,
                    LongPollServerInfo.ElementAt(0).server.ToString(),
                    LongPollServerInfo.ElementAt(0).key.ToString(),
                    LongPollServerInfo.ElementAt(0).ts.ToString());

                foreach (List<object> update in LongPollServerAnswer.ElementAt(0).updates)
                {
                    Debug.WriteLine(update.ElementAt(0).ToString());
                    if (Convert.ToInt32(update.ElementAt(0)) == 61)
                    {
                        Debug.WriteLine("Он " + update.ElementAt(1).ToString() + " набирает сообщение");
                    }
                    if (Convert.ToInt32(update.ElementAt(0)) == 4)
                    {
                        //if (currentFriendID == update.ElementAt(3).ToString())
                        //{
                        //    HistoryModel history = new HistoryModel();
                        //    history.date = Convert.ToInt32(update.ElementAt(3));
                        //    history.text = update.ElementAt(6).ToString();
                        //    history.from_id = Convert.ToInt32(update.ElementAt(3));
                        //    if (update.ElementAt(3).ToString() == _UserID)
                        //    {
                        //        history.isSelf = true;
                        //        history.imageUrl = "https://sun9-28.userapi.com/impg/c858132/v858132220/1db0dd/Z1zeI1bjQ_I.jpg?size=200x0&quality=90&sign=aec2bd4ee207db01b7b99a4036808533";
                        //    }
                        //    else
                        //    {
                        //        history.isSelf = false;
                        //        history.imageUrl = currentFriendPhoto;
                        //    }
                        //    historyModels.Add(history);

                        //}
                        for (int i = 0; i < conversationsModels.Count(); i++)
                        {
                            ConversationsModel friend = conversationsModels[i];
                            if (friend.PeerId == Convert.ToInt32(update.ElementAt(3)))
                            {
                                friend.Message = update.ElementAt(6).ToString();
                                conversationsModels[i] = friend;
                            }
                        }

                    }
                }
            }
        }

        private async Task GetConversations()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri requestUri = new Uri("https://api.vk.com/method/messages.getConversations?count=20" +
                "&filter=all" +
                "&extended=1" +
                "&access_token=" +
                _Token + __VKAPI);

            httpResponse = await httpClient.GetAsync(requestUri);
            httpResponse.EnsureSuccessStatusCode();
            string resultResponse = await httpResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ConversationWrapper>(resultResponse);
            foreach (var item in result.response.items)
            {
                ConversationsModel friend = new ConversationsModel();
                friend.PeerId = item.conversation.peer.id;
                friend.Type = item.conversation.peer.type;
                friend.Date = item.last_message.date.ToString();
                friend.FromId = item.last_message.from_id;
                friend.Message = item.last_message.text;
                friend.ConversationMessageId = item.last_message.conversation_message_id;

                for (int i = 0; i < result.response.profiles.Count(); i++)
                {
                    if (result.response.profiles[i].id == item.conversation.peer.id)
                    {
                        friend.FirstName = result.response.profiles[i].first_name;
                        friend.LastName = result.response.profiles[i].last_name;
                        friend.FullName = result.response.profiles[i].first_name + " " + result.response.profiles[i].last_name;
                        friend.Photo100 = result.response.profiles[i].photo_100;
                        friend.Online = 0;
                    }
                }

                if (friend.FullName == null)
                {
                    friend.FullName = "Беседы с батюшкой";
                    friend.Photo100 = "https://pbs.twimg.com/media/ET64qNqXsAAVKzR.jpg";
                }

                conversationsModels.Add(friend);
            }
            listConversations.ItemsSource = conversationsModels;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await RunLongPollRequests();
            ShadowTheme.Receivers.Add(MessageFrame);
        }



        private void listConversations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            friendList = (ConversationsModel)item.SelectedItem;
            if (friendList != null)
            {
                if (MessageFrame.CanGoBack)
                {
                    MessageFrame.GoBack();
                    MessageFrame.Navigate(typeof(HistoryPage), friendList, new DrillInNavigationTransitionInfo());
                } else
                {
                    MessageFrame.Navigate(typeof(HistoryPage), friendList, new DrillInNavigationTransitionInfo());
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayerElement player = new MediaPlayerElement();
            player.Source = MediaSource.CreateFromUri(new Uri("https://psv4.userapi.com/c205316//u300675947/audiomsg/d12/b1e9c83095.ogg"));
            player.MediaPlayer.Play();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
