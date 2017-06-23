using LinqToTwitter;
using Microsoft.Extensions.Options;

namespace TwitterFeedReader.Models
{
    public class TwitterAuthorizer : SingleUserAuthorizer
    {
        public TwitterAuthorizer(IOptions<AppKeyConfig> appKeys) : base()
        {
            CredentialStore = new SingleUserInMemoryCredentialStore
            {
                ConsumerKey = appKeys.Value.TwitterConsumerKey,
                ConsumerSecret = appKeys.Value.TwitterConsumerSecret,
                AccessToken = appKeys.Value.TwitterAccessToken,
                AccessTokenSecret = appKeys.Value.TwitterAccessTokenSecret
            };
        }
    }
}