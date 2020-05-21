using GibbonVk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GibbonVk.Support
{
    public class MessageAttachmentsItemDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate AttachmentsAudioMessageDataTemplate { get; set; }

        public DataTemplate AttachmentsPhotoDataTemplate { get; set; }

        public DataTemplate AttachmentsAudioDataTemplate { get; set; }

        public DataTemplate AttachmentsVideoDataTemplate { get; set; }

        public DataTemplate AttachmentsDocDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is AttachmentsHistory)
            {
                switch ((item as AttachmentsHistory).type)
                {
                    case "audio_message":
                        return AttachmentsAudioMessageDataTemplate;
                    case "photo": return AttachmentsPhotoDataTemplate;
                    case "audio": return AttachmentsAudioDataTemplate;
                    case "video": return AttachmentsVideoDataTemplate;
                    case "doc": return AttachmentsDocDataTemplate;
                    //case "wall": return App.Current.Resources["AttachmentsDocDataTemplate"] as DataTemplate;
                    default: break;
                }
            }
            return null;
        }
    }
}
