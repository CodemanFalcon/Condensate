using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Condensate.SteamApi.ResponseModels
{
    public class GameNewsResponse
    {
        public GameNews appnews { get; set; }
    }

    public class GameNews
    {
        public int appid { get; set; }
        public List<GameNewsItem> newsitems { get; set; }
    }

    public class GameNewsItem
    {
        public string gid { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool is_external_url { get; set; }
        public string author { get; set; }
        public string contents { get; set; }
        public string feedlabel { get; set; }
        public int date { get; set; }
        public DateTime dateTime { get; set; }
        public string feedname { get; set; }
        public int feed_type { get; set; }
        public int appid { get; set; }
        public Game gameProfile { get; set; }
    }

    public static class GameNewsItemExtensions
    {
        public static void ReplaceUrlInContents(this GameNewsItem news)
        {
            int first = 0;
            int second = 0;
            while (first != -1)
            {
                first = news.contents.IndexOf("[url=", second);
                if (first != -1)
                {
                    second = news.contents.IndexOf("]", first);
                    news.contents = news.contents.Remove(second, 1).Insert(second, "\">");
                }
            }
            news.contents = news.contents.Replace("[url=", "<a href=\"");
            news.contents = news.contents.Replace("[/url]", "</a>");
        }
    }


    //<iframe width = "560" height="315" src="https://www.youtube.com/embed/Uz9Vl2VFCX0" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
}
