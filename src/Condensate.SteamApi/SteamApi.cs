using Condensate.SteamApi.ResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Condensate.SteamApi
{
    public class SteamApiClient
    {
        private readonly HttpClient _client;

        public SteamApiClient()
        {
            _client = new HttpClient();
        }

        public CommunityProfileGamesResponse GetCommunityProfileGames(string communityid)
        {
            var url = $"https://steamcommunity.com/id/{communityid}/games/?xml=1";
            XmlSerializer ser = new XmlSerializer(typeof(CommunityProfileGamesResponse));

            WebClient client = new WebClient();

            string data = Encoding.Default.GetString(client.DownloadData(url));

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));

            CommunityProfileGamesResponse reply = (CommunityProfileGamesResponse)ser.Deserialize(stream);

            return reply;
        }

        public async Task<GameNewsResponse> GetGameNewsAsync(int appid)
        {
            var response = await _client
            .GetAsync($"https://api.steampowered.com/ISteamNews/GetNewsForApp/v2?appid={appid}")
            .ConfigureAwait(false);

            var gameNews = JsonConvert.DeserializeObject<GameNewsResponse>(await response.Content.ReadAsStringAsync());

            foreach(var game in gameNews.appnews.newsitems)
            {
                game.dateTime = UnixTimeStampToDateTime(game.date);
            }

            return gameNews;
        }

        public GameNewsResponse GetGameNews(int appid, int count = 0)
        {
            var url = $"https://api.steampowered.com/ISteamNews/GetNewsForApp/v2?appid={appid}";
            if (count != 0)
            {
                url = $"https://api.steampowered.com/ISteamNews/GetNewsForApp/v2?appid={appid}&count={count}";
            }
            
            var gameNewItems = new List<GameNewsItem>();
            var response = _client.GetAsync(url).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var gameNewsResponse = JsonConvert.DeserializeObject<GameNewsResponse>(json);
            foreach (var game in gameNewItems)
            {
                game.dateTime = UnixTimeStampToDateTime(game.date);
            }
            return gameNewsResponse;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
