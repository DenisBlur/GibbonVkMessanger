using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GibbonVk.Models
{
    public class LongPollAnswer
    {
        public int ts { get; set; }
        public List<List<object>> updates { get; set; }
    }
}
