using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        private MVC_Gas_MapEntities db = new MVC_Gas_MapEntities();

        // GET: api/User
        public IQueryable<UserInSystem> GetUserInSystems()
        {
            return db.UserInSystems;
        }

        // GET: api/User?username=x&&password=x
        [ResponseType(typeof(UserInSystem))]
        public IHttpActionResult GetUserInSystem(string username,string password)
        {
            if (username =="" || password=="")
            {
                return Content(HttpStatusCode.NotFound, ""); 
            }
            UserInSystem userInSystem = db.UserInSystems.FirstOrDefault(x=>x.UserName == username && x.Password == password);
            if (userInSystem == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }
            Guest guest = db.Guests.FirstOrDefault(x => x.UserID == userInSystem.UserID);
            if (guest == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }
            Image img = db.Images.FirstOrDefault(x => x.HostObjID == guest.GuestID);
            if (img == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }
            string strResponseMess = userInSystem.PermissionID.ToString();
            strResponseMess +=  ";" + userInSystem.UserID.ToString();
            strResponseMess +=  ";" + guest.GuestName.ToString();
            strResponseMess += ";" + img.ImageSrc.ToString();
            return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent
                (
                    strResponseMess,
                    Encoding.UTF8,
                    "text/html"
                )
            });
        }

        // PUT: api/User/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserInSystem(int id, UserInSystem userInSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInSystem.ID)
            {
                return BadRequest();
            }

            db.Entry(userInSystem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInSystemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/User
        [ResponseType(typeof(UserInSystem))]
        public IHttpActionResult PostUserInSystem(UserInSystem userInSystem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = db.UserInSystems.FirstOrDefault(x => x.UserName == userInSystem.UserName);
            if(user != null)
            {
                return Content(HttpStatusCode.Conflict, "Đã có user");
            }
            db.UserInSystems.Add(userInSystem);
            db.SaveChanges();
            user = db.UserInSystems.FirstOrDefault(x => x.UserName == userInSystem.UserName);
            if (user != null)
            {
                Guest guest = new Guest();
                guest.UserID = user.UserID;
                guest.CreatedDate = DateTime.Now;
                guest.GuestName = "";
                guest.GuestAddress = "";
                guest.GuestPhone = "";
                guest.GuestSex = 0;
                guest.GuestEmail = "";
                guest.GuestBirthday = DateTime.Today;

                db.Guests.Add(guest);
                db.SaveChanges();

                var guestInSys = db.Guests.FirstOrDefault(x => x.UserID==user.UserID);

                Image img = new Image();
                img.HostObjID = guestInSys.GuestID;
                img.ImageSrc = "/Images/avataaars-2.png";

                db.Images.Add(img);
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = userInSystem.ID }, userInSystem);
        }

        // DELETE: api/User/5
        [ResponseType(typeof(UserInSystem))]
        public IHttpActionResult DeleteUserInSystem(int id)
        {
            UserInSystem userInSystem = db.UserInSystems.Find(id);
            if (userInSystem == null)
            {
                return NotFound();
            }

            db.UserInSystems.Remove(userInSystem);
            db.SaveChanges();

            return Ok(userInSystem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserInSystemExists(int id)
        {
            return db.UserInSystems.Count(e => e.ID == id) > 0;
        }
    }
}