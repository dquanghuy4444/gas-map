using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC_Gas_Map.Models;
namespace MVC_Gas_Map.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Login/Create
        [HttpPost]
        public JsonResult Create(User user)
        {
            user.CreatedDate = DateTime.Now;
            HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("User", user).Result;
            if(response.StatusCode == HttpStatusCode.Created)
                return Json(new { status = 0 });
            else if (response.StatusCode == HttpStatusCode.Conflict)
                return Json(new { status = 1 });
            else return Json(new { status = 2 });
        }

        // GET: Login/Get
        [HttpPost]
        public JsonResult Get(User user)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("User?username="+user.UserName+"&&password="+user.Password).Result;
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                var permissId = Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
                return Json(new { status = 0,permissionID= permissId });
            }

            else if (response.StatusCode == HttpStatusCode.Forbidden)
                return Json(new { status = 1 });
            else return Json(new { status = 2 });
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
