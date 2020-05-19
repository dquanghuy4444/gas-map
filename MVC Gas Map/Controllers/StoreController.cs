using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC_Gas_Map.Models;
namespace MVC_Gas_Map.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores?mode=1").Result;

            string amountOfStores = response.Content.ReadAsStringAsync().Result ;
            ViewBag.AmountOfStores = amountOfStores;
            return View();
        }

        [HttpPost]
        public ActionResult getAllStores()
        {
            IEnumerable<Store> storeList;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Stores").Result;
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


        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Store/Create
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
