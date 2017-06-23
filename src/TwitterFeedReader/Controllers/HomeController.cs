using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TwitterFeedReader.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TwitterFeedReader.Controllers
{
    public class HomeController : Controller
    {
        ITweetRepository _tweetRepository;
        public HomeController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            int maxEmbeddedDetailWidth = 100;

            List<Tweet> tweets = _tweetRepository.GetLatestTweetsForScreenName(numberOfTweets: 10, screenName: "salesforce", oembedMaxWidthConstraint: maxEmbeddedDetailWidth);
            
            return View(tweets);
        }
    }
}
