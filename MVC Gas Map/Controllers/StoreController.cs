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

    public class StoreController : Controller
    {
        // GET: Store
        [HandleError]
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?mode=1").Result;
            string amountOfStores = response.Content.ReadAsStringAsync().Result ;
            ViewBag.AmountOfStores = amountOfStores;

            response = GlobalVariables.webApiClient.GetAsync("Stores?mode=2").Result;
            string amountOfUsers = response.Content.ReadAsStringAsync().Result;
            ViewBag.AmountOfUsers = amountOfUsers;

            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getAllStores()
        {
            IEnumerable<Store> storeList;
            try
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores").Result;
                storeList = response.Content.ReadAsAsync<IEnumerable<Store>>().Result;
            }
            catch
            {
                storeList = null;
            }
            //return View(storeList);
            return Json(new { data = storeList }, JsonRequestBehavior.AllowGet);
        }


        // GET: Store/Details/5
        [HttpPost]
        public ActionResult getDetailStore(string storeId)
        {
            IEnumerable<Store> storeList;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?storeId="+storeId).Result;
            try
            {
                storeList = response.Content.ReadAsAsync<IEnumerable<Store>>().Result;
            }
            catch
            {
                storeList = null;
            }
            //return View(storeList);
            return Json(new { data = storeList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();

        }

        // GET: Store/Create
        public ActionResult CreateStore(Store store)
        {
            string message = "";
            int permissionID = 3;
            int flagCheckStatus = 0; //0:fail  1:success
            store.CreatedDate = DateTime.Now;
            try
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("Stores", store).Result;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    flagCheckStatus = 0;
                else if (response.StatusCode == HttpStatusCode.OK)
                    flagCheckStatus = 1;
                message = response.Content.ReadAsStringAsync().Result;

                response = GlobalVariables.webApiClient.GetAsync("User?userID=" + store.UserID).Result;
                if (response.StatusCode == HttpStatusCode.Accepted)
                    permissionID = Convert.ToInt32(response.Content.ReadAsStringAsync().Result);

            }
            catch
            {
                 message = "";
                 permissionID = 3;
                 flagCheckStatus = 0; //0:fail  1:success
            }

            return Json(new { status = flagCheckStatus, message= message, permissionID= permissionID }, JsonRequestBehavior.AllowGet);

        }

        // POST: Store/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
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

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
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
