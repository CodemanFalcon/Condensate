using Condensate.SteamApi;
using Condensate.SteamApi.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condensate.Mvc.Controllers
{
    public class NewsFeedController : Controller
    {
        private SteamApiClient _steamApi;

        public NewsFeedController()
        {
            _steamApi = new SteamApiClient();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> InputCommunityIdForNewsFeed(string communityid, int count)
        {
            var myGames = _steamApi.GetCommunityProfileGames(communityid);
            myGames.Games.GameList = myGames.Games.GameList.OrderBy(x => int.Parse(x.AppID)).ToList();
            var newsFeed = new List<GameNewsItem>();


            //thread this to make it go faster
            var games = new List<GameNewsResponse>();
            var batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)myGames.Games.GameList.Count() / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = myGames.Games.GameList.Skip(i * batchSize).Take(batchSize);
                var tasks = currentIds.Select(game => _steamApi.GetGameNewsAsync(int.Parse(game.AppID)));
                games.AddRange(await Task.WhenAll(tasks));
            }

            foreach(var gameNewsResponse in games)
            {
                newsFeed.AddRange(gameNewsResponse.appnews.newsitems);
            }

            foreach (var newsFeedItem in newsFeed)
            {
                newsFeedItem.gameProfile = myGames.Games.GameList.Where(x => x.AppID == newsFeedItem.appid.ToString()).FirstOrDefault();
                if(newsFeedItem.gameProfile == null)
                {
                    newsFeedItem.gameProfile = new Game
                    {
                        Name = ""
                    };
                }
                else if(newsFeedItem.gameProfile.Name == null)
                {
                    newsFeedItem.gameProfile.Name = "";
                }
            }
            newsFeed.RemoveAll(x => x.gameProfile.Name == "");

            newsFeed = newsFeed.OrderByDescending(x => x.date).ToList();

            foreach (var newsItem in newsFeed)
            {
                newsItem.contents = newsItem.contents.Replace("{STEAM_CLAN_IMAGE}", "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/clans/");
                newsItem.contents = newsItem.contents.Replace("[img]", "<img class=\"gameNewsContentImage\" src=\"");
                newsItem.contents = newsItem.contents.Replace("[/img]", "\" alt/ width=\"668\" height=\"376\">");
                newsItem.contents = newsItem.contents.Replace("[list]", "<ul>");
                newsItem.contents = newsItem.contents.Replace("[/list]", "</ul>");
                newsItem.contents = newsItem.contents.Replace("[olist]", "<ol>");
                newsItem.contents = newsItem.contents.Replace("[/olist]", "</ol>");
                newsItem.contents = newsItem.contents.Replace("[*]", "<li>");
                newsItem.contents = newsItem.contents.Replace("[/*]", "</li>");
                newsItem.contents = newsItem.contents.Replace("[b]", "<b>");
                newsItem.contents = newsItem.contents.Replace("[/b]", "</b>");
                newsItem.contents = newsItem.contents.Replace("[i]", "<i>");
                newsItem.contents = newsItem.contents.Replace("[/i]", "</i>");
                newsItem.contents = newsItem.contents.Replace("[u]", "<u>");
                newsItem.contents = newsItem.contents.Replace("[/u]", "</u>");
                newsItem.contents = newsItem.contents.Replace("\n", "<br />");
                newsItem.contents = newsItem.contents.Replace("[h1]", "<h1>");
                newsItem.contents = newsItem.contents.Replace("[/h1]", "</h1>");
                newsItem.contents = newsItem.contents.Replace("[h2]", "<h2>");
                newsItem.contents = newsItem.contents.Replace("[/h2]", "</h2>");
                newsItem.contents = newsItem.contents.Replace("[h3]", "<h3>");
                newsItem.contents = newsItem.contents.Replace("[/h3]", "</h3>");
                


                newsItem.ReplaceUrlInContents();
            }

            return View(newsFeed);
        }
    }
}
