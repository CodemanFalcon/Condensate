using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Condensate.SteamApi.ResponseModels
{
    [XmlRoot(ElementName = "gamesList")]
    public class CommunityProfileGamesResponse
    {
        [XmlElement(ElementName = "steamID64")]
        public string SteamID64 { get; set; }
        [XmlElement(ElementName = "steamID")]
        public string SteamID { get; set; }
        [XmlElement(ElementName = "games")]
        public Games Games { get; set; }
    }

    [XmlRoot(ElementName = "games")]
    public class Games
    {
        [XmlElement(ElementName = "game")]
        public List<Game> GameList { get; set; }
    }

	[XmlRoot(ElementName = "game")]
	public class Game
	{
		[XmlElement(ElementName = "appID")]
		public string AppID { get; set; }
		[XmlElement(ElementName = "name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "logo")]
		public string Logo { get; set; }
		[XmlElement(ElementName = "storeLink")]
		public string StoreLink { get; set; }
		[XmlElement(ElementName = "hoursLast2Weeks")]
		public string HoursLast2Weeks { get; set; }
		[XmlElement(ElementName = "hoursOnRecord")]
		public string HoursOnRecord { get; set; }
		[XmlElement(ElementName = "statsLink")]
		public string StatsLink { get; set; }
		[XmlElement(ElementName = "globalStatsLink")]
		public string GlobalStatsLink { get; set; }
	}
}
