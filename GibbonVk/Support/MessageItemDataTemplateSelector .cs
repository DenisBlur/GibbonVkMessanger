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

        public DataTemplate ReplyPhotoMessageDataTemplate { get; set; }

        public DataTemplate ReplyAudioMessageDataTemplate { get; set; }

        public DataTemplate ReplyMessageDataTemplate { get; set; }

        public DataTemplate StickerMessageDataTemplate { get; set; }

        public DataTemplate AudioMessageDataTemplate { get; set; }

        public DataTemplate PhotoGridMessageDataTemplate { get; set; }

        public DataTemplate PhotoMessageDataTemplate { get; set; }

        public DataTemplate MessageDataTemplate { get; set; }

        public DataTemplate SelfReplyMessageDataTemplate { get; set; }

        public DataTemplate SelfAttachmentsMessageDataTemplate { get; set; }

        public DataTemplate SelfMessageDataTemplate { get; set; }

        public DataTemplate SelfReplyPhotoMessageDataTemplate { get; set; }

        public DataTemplate SelfReplyAudioMessageDataTemplate { get; set; }


        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is HistoryModel)
            {
                if ((item as HistoryModel).isSelf == false)
                {
                    if ((item as HistoryModel).replyMessage != null)
                    {
                        if ((item as HistoryModel).replyMessage.attachments.Count != 0)
                        {
                            switch ((item as HistoryModel).replyMessage.attachments[0].type)
                            {
                                case "photo": return ReplyPhotoMessageDataTemplate;
                                case "audio_message": return ReplyAudioMessageDataTemplate;
                            }
                        } else return ReplyMessageDataTemplate;
                    }
                    else
                    {
                        if ((item as HistoryModel).attachmentsHistories != null)
                        {
                            switch ((item as HistoryModel).attachmentsHistories[0].type)
                            {
                                case "sticker":
                                    return StickerMessageDataTemplate;
                                case "audio_message":
                                    return AudioMessageDataTemplate;
                                case "photo":
                                    if ((item as HistoryModel).attachmentsHistories.Count > 1)
                                    {
                                        return PhotoGridMessageDataTemplate;
                                    }
                                    else
                                    {
                                        return PhotoMessageDataTemplate;
                                    }
                                default: break;
                            }
                        }
                        else return MessageDataTemplate;
                    }
                }










                else
                {
                    if ((item as HistoryModel).replyMessage != null)
                    {
                        if ((item as HistoryModel).replyMessage.attachments.Count != 0)
                        {
                            switch ((item as HistoryModel).replyMessage.attachments[0].type)
                            {
                                case "photo": return SelfReplyPhotoMessageDataTemplate;
                                case "audio_message": return SelfReplyAudioMessageDataTemplate;
                            }
                        }
                        else return SelfReplyMessageDataTemplate;
                    }
                    else
                    {
                        if ((item as HistoryModel).attachmentsHistories != null)
                        {
                            return SelfAttachmentsMessageDataTemplate;
                            //switch ((item as HistoryModel).attachmentsHistories[0].type)
                            //{
                            //    case "sticker":
                            //        return App.Current.Resources["SelfStickerMessageDataTemplate"] as DataTemplate;
                            //    case "audio_message":
                            //        return App.Current.Resources["SelfAudioMessageDataTemplate"] as DataTemplate;
                            //    case "photo":
                            //        if ((item as HistoryModel).attachmentsHistories.Count > 1)
                            //        {
                            //            return App.Current.Resources["SelfPhotoGridMessageDataTemplate"] as DataTemplate;
                            //        }
                            //        else
                            //        {
                            //            return App.Current.Resources["SelfPhotoMessageDataTemplate"] as DataTemplate;
                            //        }
                            //    default: break;
                            //}
                        }
                        else return SelfMessageDataTemplate;
                    }
                }
            }
            return null;
        }
    }
}
