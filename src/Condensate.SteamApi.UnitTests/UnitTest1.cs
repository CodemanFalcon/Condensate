using System;
using Xunit;

namespace Condensate.SteamApi.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var sut = new SteamApiClient();
            var result = sut.GetGameNews(1159560);
            var news = result.appnews.newsitems;

        }
    }
}
