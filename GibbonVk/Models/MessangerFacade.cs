using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static GibbonVk.MainPage;

namespace GibbonVk.Models
{
    class MessangerFacade
    {

        private const string MethodURL = "https://api.vk.com/method";

        public static async Task PopulateLongPollServerInfoAsync(ObservableCollection<LongPollServerResponse> longPollServerInfoObservableCollection)
        {
            try
            {
                var longPollServerInfoWrapper = await GetLongPollServerWrapperAsync();
                var longPollSererInfo = longPollServerInfoWrapper.response;
                longPollServerInfoObservableCollection.Add(longPollSererInfo);
            }
            catch (Exception)
            {
                return;
            }
        }

        private static async Task<LongPollServerWrapper> GetLongPollServerWrapperAsync()
        {
            var url = String.Format("{0}/messages.getLongPollServer?access_token={1}&{2}", MethodURL, _Token, __VKAPI);
            var jsonMessage = await CallVKAsync(url);
            var result = JsonConvert.DeserializeObject <LongPollServerWrapper>(jsonMessage);
            return result;
        }

        public static async Task PopulateLongPollAnswersAsync(ObservableCollection<LongPollAnswer> longPollServerAnswersObservableCollection, string server, string key, string ts)
        {
            try
            {
                var longPollServerAnswersWrapper = await GetLongPollAnswersAsync(server, key, ts);

                longPollServerAnswersObservableCollection.Add(longPollServerAnswersWrapper);
            }
            catch (Exception)
            {

                return;
            }
        }

        private static async Task<LongPollAnswer> GetLongPollAnswersAsync(string server, string key, string ts)
        {
            var url = String.Format("http://{0}?act=a_check&key={1}&ts={2}&wait=25&mode=2", server, key, ts);
            var jsonMessage = await CallVKAsync(url);

            var result = JsonConvert.DeserializeObject<LongPollAnswer>(jsonMessage);

            return result;
        }

        public static async Task<string> CallVKAsync(string url)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
