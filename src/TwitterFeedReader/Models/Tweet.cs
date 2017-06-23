using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterFeedReader.Models
{
    public class Tweet
    {
        public ulong ID { get; set; }
        public string Text{ get; set; }
        public string ScreenName { get; set; }
        public string HTMLForOEmbed { get; set; }
    }
}
