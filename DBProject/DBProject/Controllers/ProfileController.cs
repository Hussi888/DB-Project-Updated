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
    public class ProfileController : Controller
    {
        public ActionResult Profile()
        {
            string username = (string)Session["username"];
            ProfileModel Profile = new ProfileModel();
            Profile.user = DBProject.Models.Profile.DisplayInfo(username);
            Profile.allMovies = DBProject.Models.CRUD.getMovies();
            Profile.allTVShows = DBProject.Models.CRUD.getTVShows();
            return View(Profile);
        }
        [HttpPost]
        public ActionResult Profile(string username)
        {
            Session["username"] = username;
            Session.Abandon(); // it will clear the session at the end of request
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}