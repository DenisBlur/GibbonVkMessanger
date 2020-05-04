using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{

    public class ImagesStickerHistory
    {
        public string url { get; set; }
    }

    public class StickerHistory
    {
        public List<ImagesStickerHistory> images { get; set; }
    }
    
    public class SizesPhotosHistory
    {
        public string url { get; set; }
    }

    public class PhotoHistory
    {
        public List<SizesPhotosHistory> sizes { get; set; }
    }

    public class AudioMessageHistory
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public int duration { get; set; }
        public string link_mp3 { get; set; }
    }

    public class WallHistory
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int to_id { get; set; }
        public int date { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public List<AttachmentsHistory> attachments { get; set; }
    }

    public class VideoHistory
    {
        public int can_comment { get; set; }
        public int can_like { get; set; }
        public int can_repost { get; set; }
        public int can_subscribe { get; set; }
        public int can_add_to_faves { get; set; }
        public int can_add { get; set; }
        public int comments { get; set; }
        public int date { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public List<SizesPhotosHistory> image { get; set; }
    }

    public class AttachmentsHistory
    {
        public string type { get; set; }
        public StickerHistory sticker { get; set; }
        public PhotoHistory photo { get; set; }
        public AudioMessageHistory audio_message { get; set; }
        public WallHistory wall { get; set; }
        public VideoHistory video { get; set; }
    }

    public class ReplyMessage
    {
        public int date { get; set; }
        public int from_id { get; set; }
        public string text { get; set; }
        public List<AttachmentsHistory> attachments { get; set; }
    }

    public class ItemHistory
    {
        public int date { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int peer_id { get; set; }
        public string text { get; set; }
        public int conversation_message_id { get; set; }
        public List<AttachmentsHistory> attachments { get; set; }
        public ReplyMessage reply_message { get; set; }
    }

    public class ResponseHistory
    {
        public int count { get; set; }
        public List<ItemHistory> items { get; set; }
    }

    class HistoryWrapper
    {
        public ResponseHistory response { get; set; }
    }
        
}
