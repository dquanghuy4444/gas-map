using MVC_Gas_Map.Class;
using MVC_Gas_Map.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Gas_Map.Controllers
{
    public class UserController : Controller
    {

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            user.Password = Common.CreateMD5(user.Password);
            user.NewPassword = Common.CreateMD5(user.NewPassword);
            HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("User", user).Result;
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Json(new { status = 1 });
            return Json(new { status = 0 });
        }
    }
}
