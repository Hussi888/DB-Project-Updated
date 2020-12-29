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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (!CRUD.Login(user))
            {
                ViewBag.Message = "Login Failed";
                return View();
            }
            Session["username"] = user.username;
            return RedirectToAction("Profile", "Profile");
        }
        [HttpPost]
        public ActionResult Signup(Users user)
        {
            if (!CRUD.Signup(user))
            {
                ViewBag.Message = "Signup Failed";
                return View();
            }
            Session["username"] = user.username;
            return RedirectToAction("Profile", "Profile");
        }

    }
}