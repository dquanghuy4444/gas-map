using MVC_Gas_Map.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVC_Gas_Map.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Index()
        {
            return View();
        }

        // GET: Guest/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Guest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Guest/Edit
        [HttpGet]
        public JsonResult Edit(Guest guest)
        {
            HttpResponseMessage response =  GlobalVariables.webApiClient.PutAsJsonAsync("Guests", guest).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string strResponseMess = response.Content.ReadAsStringAsync().Result;
                var arrResponseMess = strResponseMess.Split(';');
                var permissID = Convert.ToInt32(arrResponseMess[0]);
                var guestName = arrResponseMess[1].ToString();
                return Json(new { status = 0 ,permissionId = permissID ,guestName = guestName }, JsonRequestBehavior.AllowGet);
            }
            else return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
        }

        // GET: Guest/Edit
        [HttpGet]
        public JsonResult SetPermissionID(User user)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("User", user).Result;
            if (response.StatusCode == HttpStatusCode.OK)
                return Json(new { status = 0 }, JsonRequestBehavior.AllowGet);
            else return Json(new { status = 1 }, JsonRequestBehavior.AllowGet);
        }

        // POST: Guest/Edit/5
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

        // GET: Guest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Guest/Delete/5
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

        //GET: Login/Get
        [HttpPost]
        public JsonResult Get(string userID)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("guests?UserID=" + userID).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var contents = response.Content.ReadAsStringAsync().Result;

                var guestJson = JsonConvert.DeserializeObject<Guest>(contents);
                return Json(guestJson);
            }

            else return Json(new { status = 1 });
        }
    }
}
