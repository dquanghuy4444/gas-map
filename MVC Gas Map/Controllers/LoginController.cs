using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC_Gas_Map.Models;
using MVC_Gas_Map.Class;
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
            user.Password = HashByMd5.CreateMD5(user.Password);
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
            string strQuery= "username="+user.UserName+"&&password="+ HashByMd5.CreateMD5(user.Password); 
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("User?"+strQuery).Result;
            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                string strResponseMess=response.Content.ReadAsStringAsync().Result;
                var arrResponseMess = strResponseMess.Split(';');
                var permissID = Convert.ToInt32(arrResponseMess[0]);
                var userID = arrResponseMess[1].ToString();
                var guestName = arrResponseMess[2].ToString();
                var imgSrc = arrResponseMess[3].ToString();
                return Json(new { status = 0,permissionID = permissID ,userID = userID, guestName = guestName , imgSrc = imgSrc });
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
