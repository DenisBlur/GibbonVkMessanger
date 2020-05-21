using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    public class AttachmentsHistory : INotifyPropertyChanged
    {
        private RelayCommandGenericType<AttachmentsHistory> _openPhotoCommand;
        RelayCommandGenericType<AttachmentsHistory> OpenPhotoCommand { get { return _openPhotoCommand; } set { _openPhotoCommand = value; } }

        public AttachmentsHistory()
        {
            OpenPhotoCommand = new RelayCommandGenericType<AttachmentsHistory>(DoOpenPhotoCommand);
        }

        private void DoOpenPhotoCommand(AttachmentsHistory attachmentsHistory)
        {
            Debug.WriteLine(attachmentsHistory.type);
        }

        public string type { get; set; }
        public StickerHistory sticker { get; set; }
        public PhotoHistory photo { get; set; }
        public AudioMessageHistory audio_message { get; set; }
        public WallHistory wall { get; set; }
        public VideoHistory video { get; set; }
        public AudioHistory audio { get; set; }
        public DocHistory doc { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
