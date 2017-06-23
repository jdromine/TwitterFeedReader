using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterFeedReader.Models
{
    public interface ITweetRepository
    {
        List<Tweet> GetLatestTweetsForScreenName(int numberOfTweets, string screenName, int oembedMaxWidthConstraint);
    }
}
