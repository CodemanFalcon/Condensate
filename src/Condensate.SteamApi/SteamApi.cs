using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Condensate.SteamApi
{
    public class SteamApiClient
    {

        public SteamApiClient()
        {

        }

        public GameNewsResponse GetGameNews(int appid)
        {
            var url = $"https://api.steampowered.com/ISteamNews/GetNewsForApp/v2?appid={appid}";
            var gameNewItems = new List<GameNewsItem>();
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                var gameNewsResponse = JsonSerializer.Deserialize<GameNewsResponse>(json);
                return gameNewsResponse;
            }
        }

        public class GameNewsResponse {
            public GameNews appnews { get; set; }
        }

        public class GameNews
        {
            public int appid { get; set; }
            public List<GameNewsItem> newsitems { get; set; }
        }

        public class GameNewsItem{
            public string gid { get; set; }
            public string title { get; set; }
            public string url { get; set; }
            public bool is_external_url { get; set; }
            public string author { get; set; }
            public string contents { get; set; }
            public string feedlabel { get; set; }
            public int date { get; set; }
            public string feedname { get; set; }
            public int feed_type { get; set; }
            public int appid { get; set; }
           
        }
    }
}
