using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TwitterFeedReader.Models
{
    public class TweetRepository : ITweetRepository
    {
        private TwitterContext _twitterContext;
        private TwitterAuthorizer _auth;
        public TweetRepository(TwitterAuthorizer auth)
        {
            _auth = auth;
            _twitterContext = new TwitterContext(_auth);
        }

        public List<Tweet> GetLatestTweetsForScreenName(int numberOfTweets, string screenName, int oembedMaxWidthConstraint)
        {
            List<Tweet> tweets =
                (
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                        tweet.ScreenName == screenName
                    orderby tweet.CreatedAt descending
                    select new Tweet{ Text = tweet.Text, ID = tweet.StatusID, ScreenName = tweet.ScreenName })
                    .Take(numberOfTweets).ToList();

            foreach(Tweet tweet in tweets)
            {
                tweet.HTMLForOEmbed = GetEmbeddedTweetDetails(tweet.ID, oembedMaxWidthConstraint);
            }

            return tweets;
        }

        private string GetEmbeddedTweetDetails(ulong tweetId, int oEmbedMaxWidth)
        {
            return (from oembeddedTweet in _twitterContext.Status
                    where oembeddedTweet.Type == StatusType.Oembed &&
                        oembeddedTweet.ID == tweetId &&
                        oembeddedTweet.OEmbedMaxWidth == oEmbedMaxWidth &&
                        oembeddedTweet.OEmbedOmitScript == true

                                select oembeddedTweet.EmbeddedStatus.Html
                                    ).FirstOrDefault().ToString();
        }

    }
}