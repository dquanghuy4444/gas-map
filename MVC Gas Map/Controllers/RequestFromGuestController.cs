using MVC_Gas_Map.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Gas_Map.Controllers
{
    public class RequestFromGuestController : Controller
    {
        // GET: RequestFromGuest
        public ActionResult Index()
        {
            return View();
        }

        // GET: RequestFromGuest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequestFromGuest/Create
        public ActionResult Create(RequestFromGuest reqFrmGue)
        {
            reqFrmGue.CreatedDate = DateTime.Now;
            return View();
        }

        // POST: RequestFromGuest/Create
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

        // GET: RequestFromGuest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestFromGuest/Edit/5
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

        // GET: RequestFromGuest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestFromGuest/Delete/5
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
