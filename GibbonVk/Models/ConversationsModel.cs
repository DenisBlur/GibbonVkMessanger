using System;
using System.ComponentModel;

namespace GibbonVk.Models
{
    public class ConversationsModel : INotifyPropertyChanged
    {
        public int PeerId { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }
        public int FromId { get; set; }
        public string Message { get; set; }
        public int ConversationMessageId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo100 { get; set; }
        public int Online { get; set; }
        public string FullName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
