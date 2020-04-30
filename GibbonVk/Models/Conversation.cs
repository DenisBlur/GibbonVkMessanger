using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{

    public class Peer
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class CanWrite
    {
        public Boolean allowed { get; set; }
    }

    public class Conversation
    {
        public Peer peer { get; set; }
        public int last_message_id { get; set; }
        public int in_read { get; set; }
        public int out_read { get; set; }
        public CanWrite can_write { get; set; }
    }

    public class LastMessage
    {
        public int date { get; set; }
        public int from_id { get; set; }
        public int id { get; set; }
        public int peer_id { get; set; }
        public string text { get; set; }
        public int conversation_message_id { get; set; }
    }

    public class Item
    {
        public Conversation conversation { get; set; }
        public LastMessage last_message { get; set; }
    }

    public class Friend
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string photo_100 { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
        public List<Friend> profiles { get; set; }
    }

    public class ConversationWrapper
    {
        public Response response { get; set; }
    }
}
