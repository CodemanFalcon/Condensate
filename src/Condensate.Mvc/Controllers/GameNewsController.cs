using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Condensate.SteamApi;
using Microsoft.AspNetCore.Mvc;

namespace Condensate.Mvc.Controllers
{
    public class GameNewsController : Controller
    {
        private SteamApiClient _steamApi;

        public GameNewsController()
        {
            _steamApi = new SteamApiClient();
        }
        public IActionResult Index()
        {
            //var appid = 976730;
            //SteamApiClient.GameNewsResponse gameNews = _steamApi.GetGameNews(appid);
            //foreach(var newsItem in gameNews.appnews.newsitems)
            //{
            //    newsItem.contents = newsItem.contents.Replace("{STEAM_CLAN_IMAGE}", "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/clans/");
            //    newsItem.contents = newsItem.contents.Replace("[img]", "<img src=\"");
            //    newsItem.contents = newsItem.contents.Replace("[/img]", "\" alt/ width=\"668\" height=\"376\">");
            //    newsItem.contents = newsItem.contents.Replace("[list]", "<ul>");
            //    newsItem.contents = newsItem.contents.Replace("[/list]", "</ul>");
            //    newsItem.contents = newsItem.contents.Replace("[olist]", "<ol>");
            //    newsItem.contents = newsItem.contents.Replace("[/olist]", "</ol>");
            //    newsItem.contents = newsItem.contents.Replace("[*]", "<li>");
            //    newsItem.contents = newsItem.contents.Replace("[b]", "<b>");
            //    newsItem.contents = newsItem.contents.Replace("[/b]", "</b>");
            //    newsItem.contents = newsItem.contents.Replace("[i]", "<i>");
            //    newsItem.contents = newsItem.contents.Replace("[/i]", "</i>");
            //    newsItem.contents = newsItem.contents.Replace("\n", "<br />");
            //    newsItem.contents = newsItem.contents.Replace("[h3]", "<h3>");
            //    newsItem.contents = newsItem.contents.Replace("[/h3]", "</h3>");
            //    newsItem.contents = newsItem.contents.Replace("[h1]", "<h1>");
            //    newsItem.contents = newsItem.contents.Replace("[/h1]", "</h1>");




            //}
            return View();
        }

        [HttpGet]
        public ActionResult InputAppid(int appid)
        {
            var gameNews = _steamApi.GetGameNews(appid);
            if (gameNews.appnews != null)
            {
                foreach (var newsItem in gameNews.appnews.newsitems)
                {
                    newsItem.contents = newsItem.contents.Replace("{STEAM_CLAN_IMAGE}", "https://cdn.cloudflare.steamstatic.com/steamcommunity/public/images/clans/");
                    newsItem.contents = newsItem.contents.Replace("[img]", "<img src=\"");
                    newsItem.contents = newsItem.contents.Replace("[/img]", "\" alt/ width=\"668\" height=\"376\">");
                    newsItem.contents = newsItem.contents.Replace("[list]", "<ul>");
                    newsItem.contents = newsItem.contents.Replace("[/list]", "</ul>");
                    newsItem.contents = newsItem.contents.Replace("[olist]", "<ol>");
                    newsItem.contents = newsItem.contents.Replace("[/olist]", "</ol>");
                    newsItem.contents = newsItem.contents.Replace("[*]", "<li>");
                    newsItem.contents = newsItem.contents.Replace("[b]", "<b>");
                    newsItem.contents = newsItem.contents.Replace("[/b]", "</b>");
                    newsItem.contents = newsItem.contents.Replace("[i]", "<i>");
                    newsItem.contents = newsItem.contents.Replace("[/i]", "</i>");
                    newsItem.contents = newsItem.contents.Replace("\n", "<br />");
                    newsItem.contents = newsItem.contents.Replace("[h3]", "<h3>");
                    newsItem.contents = newsItem.contents.Replace("[/h3]", "</h3>");
                    newsItem.contents = newsItem.contents.Replace("[h1]", "<h1>");
                    newsItem.contents = newsItem.contents.Replace("[/h1]", "</h1>");
                }
                return View(gameNews);
            }
            else
            {
                return View("Index");
            }
        }
    }
}
