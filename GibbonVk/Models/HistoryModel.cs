using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    class HistoryModel : INotifyPropertyChanged
    {
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
