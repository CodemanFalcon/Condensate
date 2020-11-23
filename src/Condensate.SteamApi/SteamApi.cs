using Condensate.SteamApi.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Condensate.SteamApi
{
    public class SteamApiClient
    {

        public SteamApiClient()
        {

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
    }
}
