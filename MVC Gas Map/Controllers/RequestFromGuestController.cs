using MVC_Gas_Map.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("RequestFromGuests", reqFrmGue).Result;
            string mess = response.Content.ReadAsStringAsync().Result;
            var status = -1;
            if (response.StatusCode == HttpStatusCode.Created)
                status = 1;
            else
                status = 0;
            return Json(new { message = mess,status=status }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllRequests()
        {
            IEnumerable<RequestFromGuest> requestList;
            try
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("RequestFromGuests").Result;
                requestList = response.Content.ReadAsAsync<IEnumerable<RequestFromGuest>>().Result;
            }
            catch
            {
                requestList = null;
            }
            //return View(storeList);
            return Json(new { data = requestList }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetRequestDetail(string reqFrmGueID)
        {
            RequestFromGuest req;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("RequestFromGuests?reqFrmGueID=" + reqFrmGueID).Result;
            try
            {
                req = response.Content.ReadAsAsync<RequestFromGuest>().Result;
            }
            catch
            {
                req = null;
            }
            //return View(storeList);
            return Json(new { data = req }, JsonRequestBehavior.AllowGet);
        }
    }
}
