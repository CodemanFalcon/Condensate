using Condensate.SteamApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condensate.Mvc.Controllers
{
    public class MyGames : Controller
    {
        private SteamApiClient _steamApi;

        public MyGames()
        {
            _steamApi = new SteamApiClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InputCommunityId(string communityid)
        {
            var myGames = _steamApi.GetCommunityProfileGames(communityid);
            if (myGames.Games != null)
            {
                return View(myGames);
            }
            else
            {
                return View("Index");
            }
        }
    }

    
}
