using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Core;
using Windows.UI.Xaml.Controls;

namespace GibbonVk.Models
{
    class HistoryModel : INotifyPropertyChanged
    {

        private RelayCommandGenericType<HistoryModel> _playAudioCommand;
        public RelayCommandGenericType<HistoryModel> PlayAudioCommand { get { return _playAudioCommand; } set { _playAudioCommand = value; } }

        public HistoryModel()
        {
            PlayAudioCommand = new RelayCommandGenericType<HistoryModel>(DoPlayAudioCommand);
        }

        private void DoPlayAudioCommand(HistoryModel historyModel)
        {
            MediaPlayerElement player = new MediaPlayerElement();
            player.Source = MediaSource.CreateFromUri(new Uri(historyModel.attachmentsHistories[0].audio_message.link_mp3));
            player.MediaPlayer.Play();
        }

        public int date { get; set; }
        public int from_id { get; set; }
        public string text { get; set; }
        public string imageUrl { get; set; }
        public bool isSelf { get; set; }
        public List<AttachmentsHistory> attachmentsHistories { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
