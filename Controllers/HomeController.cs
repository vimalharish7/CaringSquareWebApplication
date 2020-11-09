using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;
using Microsoft.AspNet.Identity;

namespace CaringSquareApp.Controllers
{
    public class HomeController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        /*
         * Name: Home Page - Index()
         * First Function implemented when application is launched
         * No Arguments
         * Returns Home Page to User
         */
        public ActionResult Index()
        {
            return View();
        }

        /*
         * Name: Knowledge Hub - About()
         * Function implemented when Knowledge Hub is Clicked
         * No Arguments
         * Returns Knowledge Hub Page to User
         */
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /*
         * No Usage in Application         
         */
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*
         * Name: For Dashboard of Logged In user  - Dashboard()
         * Function implemented when Dashboard is Clicked
         * No Arguments
         * Returns Dashboard Page to Logged In User
         * Returns Event Date in Recent first Order
         */
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";

            DateTime dtFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.ToList().Where(s => s.UserUserId == userId && DateTime.ParseExact(s.EventDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture) >= dtFrom);

            //var socialEvents = db.SocialEvents.Include(s => s.AspNetUser).Include(s => s.POIs);
            return View(eventLists.OrderBy(s => s.EventDate));
        }
    }
}