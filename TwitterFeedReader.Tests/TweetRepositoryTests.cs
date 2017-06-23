using TwitterFeedReader.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace TwitterFeedReader.Tests
{
    [TestClass]
    public class TweetRepositoryTests
    {
       
        [TestMethod]
        public void GetLatestTweetsForScreenName_ShouldReturn10Tweets_WhenProvidedSalesforceScreenName()
        {
            //Arrange
            TwitterAuthorizer auth = new TwitterAuthorizer(GetAppKeyConfigOptions());
            TweetRepository rep = new TweetRepository(auth);

            //Act
            List<Tweet> tweets = rep.GetLatestTweetsForScreenName(10, "salesforce", 150);

            //Assert
            Assert.AreEqual(10, tweets.Count);
        }

        [TestMethod]
        public void GetLatestTweetsForScreenName_ShouldReturnTweetsForSalesforce_WhenProvidedSalesforceScreenName()
        {
            //Arrange
            TwitterAuthorizer auth = new TwitterAuthorizer(GetAppKeyConfigOptions());
            TweetRepository rep = new TweetRepository(auth);

            //Act
            List<Tweet> tweets = rep.GetLatestTweetsForScreenName(10, "salesforce", 150);
            List<Tweet> nonSalesForceTweets = tweets.Where(t => t.ScreenName != "salesforce").ToList();

            //Assert
            //there are tweets
            Assert.AreEqual(10, tweets.Count);

            //they are no tweets for another screen name
            Assert.AreEqual(0, nonSalesForceTweets.Count);
        }

        [TestMethod]
        public void GetLatestTweetsForScreenName_ShouldReturnEmbeddedDetailsForTweet()
        {
            //Arrange
            TwitterAuthorizer auth = new TwitterAuthorizer(GetAppKeyConfigOptions());
            TweetRepository rep = new TweetRepository(auth);

            //Act
            List<Tweet> tweets = rep.GetLatestTweetsForScreenName(10, "salesforce", 150);
            List<Tweet> missingEmbeddedDetails = tweets.Where(t => t.HTMLForOEmbed == null || t.HTMLForOEmbed == string.Empty).ToList();

            //Assert
            //there are tweets
            Assert.AreEqual(10, tweets.Count);

            //they are no tweets for another screen name
            Assert.AreEqual(0, missingEmbeddedDetails.Count);
        }

        private IOptions<AppKeyConfig> GetAppKeyConfigOptions()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("client-secret.json")
                .Build();

            AppKeyConfig appKeys = new AppKeyConfig();
            appKeys.TwitterConsumerKey = config["TwitterConsumerKey"];
            appKeys.TwitterConsumerSecret = config["TwitterConsumerSecret"];
            appKeys.TwitterAccessToken = config["TwitterAccessToken"];
            appKeys.TwitterAccessTokenSecret = config["TwitterAccessTokenSecret"];

            return Options.Create(appKeys);
        }
    }
}
