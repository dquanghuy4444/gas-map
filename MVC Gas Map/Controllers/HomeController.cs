using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Gas_Map.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Intro()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?mode=1").Result;
            string amountOfStores = response.Content.ReadAsStringAsync().Result;
            ViewBag.AmountOfStores = amountOfStores;

            response = GlobalVariables.webApiClient.GetAsync("User?mode=1").Result;
            string amountOfUsers = response.Content.ReadAsStringAsync().Result;
            ViewBag.AmountOfUsers = amountOfUsers;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}