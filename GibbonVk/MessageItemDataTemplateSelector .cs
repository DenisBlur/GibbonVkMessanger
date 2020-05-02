using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GibbonVk.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GibbonVk
{
    public class MessageItemDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is HistoryModel)
            {
                if ((item as HistoryModel).isSelf == false)
                {
                    if ((item as HistoryModel).attachmentsHistories != null)
                    {
                        switch ((item as HistoryModel).attachmentsHistories[0].type)
                        {
                            case "sticker":
                                return App.Current.Resources["StickerMessageDataTemplate"] as DataTemplate;
                            case "audio_message":
                                return App.Current.Resources["AudioMessageDataTemplate"] as DataTemplate;
                            case "photo":
                                if ((item as HistoryModel).attachmentsHistories.Count > 1)
                                {
                                    return App.Current.Resources["PhotoGridMessageDataTemplate"] as DataTemplate;
                                }
                                else
                                {
                                    return App.Current.Resources["PhotoMessageDataTemplate"] as DataTemplate;
                                }
                            default: break;
                        }
                    } else return App.Current.Resources["MessageDataTemplate"] as DataTemplate;
                } else
                {
                    if ((item as HistoryModel).attachmentsHistories != null)
                    {
                        switch ((item as HistoryModel).attachmentsHistories[0].type)
                        {
                            case "sticker":
                                return App.Current.Resources["SelfStickerMessageDataTemplate"] as DataTemplate;
                            case "audio_message":
                                return App.Current.Resources["SelfAudioMessageDataTemplate"] as DataTemplate;
                            case "photo":
                                 if ((item as HistoryModel).attachmentsHistories.Count > 1)
                                {
                                  return  App.Current.Resources["SelfPhotoGridMessageDataTemplate"] as DataTemplate;
                                } else
                                {
                                  return App.Current.Resources["SelfPhotoMessageDataTemplate"] as DataTemplate;
                                }
                            default: break;
                        }
                    }
                    else return App.Current.Resources["SelfMessageDataTemplate"] as DataTemplate;
                }
            }
            return null;
        }
    }
}
