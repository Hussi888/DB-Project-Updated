using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBProject.Models;

namespace DBProject.Controllers
{
    public class CalenderController : Controller
    {
        public ActionResult Calender()
        {
            List<Episode> allCurrentEpisodes = CRUD.GetUpcomingEpisodes();
            return View(allCurrentEpisodes);
        }
    }
}
