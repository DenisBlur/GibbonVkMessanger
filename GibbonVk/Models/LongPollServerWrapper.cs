using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    public class LongPollServerResponse
    {
        public string key { get; set; }
        public string server { get; set; }
        public int ts { get; set; }
    }

    public class LongPollServerWrapper
    {
        public LongPollServerResponse response { get; set; }
    }

}
