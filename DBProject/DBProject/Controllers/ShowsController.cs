using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using DBProject.Models;
using System.Web.Mvc;

namespace DBProject.Controllers
{
    public class ShowsController : Controller
    {
        public ActionResult Shows()
        {
            //Passing Multiple lists to VIEW
            var Lists = new ShowLists();
            Lists.allShows = DBProject.Models.CRUD.getShows();
            Lists.allMovies = DBProject.Models.CRUD.getMovies();
            Lists.allTVShows = DBProject.Models.CRUD.getTVShows();
            return View(Lists);
        }
        public ActionResult Trending()
        {
            var Lists = new ShowLists();
            Lists.allMovies = DBProject.Models.CRUD.GetTrendingMovies();
            Lists.allTVShows = DBProject.Models.CRUD.GetTrendingTVShows();
            return View(Lists);
        }
        [HttpPost]
        public ActionResult Movies()
        {
            Movies currentMovie = new Movies();
            currentMovie.movieId = ViewBag.Messege;
            return View(currentMovie);
        }
        [HttpPost]
        public ActionResult TV()
        {
            TVShows currentTVShow = new TVShows();
            currentTVShow.TVShowId = ViewBag.Messege;
            return View(currentTVShow);
        }
    }
}
