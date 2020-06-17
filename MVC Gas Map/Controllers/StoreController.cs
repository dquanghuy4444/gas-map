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
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllStores()
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


        [HttpGet]
        public ActionResult GetAllStoresOfUserID(string userID)
        {
            List<Store> list;
            try
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?userID=" + userID).Result;
                list = response.Content.ReadAsAsync<List<Store>>().Result;
            }
            catch
            {
                list = null;
            }
            //return View(storeList);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }


        // GET: Store/Details/5
        [HttpPost]
        public ActionResult GetDetailStore(string storeId)
        {
            IEnumerable<Store> store;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?storeId="+storeId).Result;
            try
            {
                store = response.Content.ReadAsAsync<IEnumerable<Store>>().Result;
            }
            catch
            {
                store = null;
            }
            //return View(storeList);
            return Json(new { data = store }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();

        }

        public ActionResult CreateAndEdit()
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

        // GET: Store/Edit/5
        public ActionResult EditStore(Store store)
        {
            string message = "";
            int flagCheckStatus = 0; //0:fail  1:success
            try
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("Stores", store).Result;
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    flagCheckStatus = 0;
                else if (response.StatusCode == HttpStatusCode.OK)
                    flagCheckStatus = 1;
                message = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                message = "";
                flagCheckStatus = 0; //0:fail  1:success
            }
            return Json(new { status = flagCheckStatus, message = message}, JsonRequestBehavior.AllowGet);
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
