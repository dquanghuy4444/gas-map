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
    public class GuestsController : ApiController
    {
        private MVC_Gas_MapEntities db = new MVC_Gas_MapEntities();

        // GET: api/Guests
        public IQueryable<Guest> GetGuests()
        {
            return db.Guests;
        }

        // GET: api/Guests?userID=x
        [ResponseType(typeof(Guest))]
        public IHttpActionResult Get(string userID)
        {
            if (userID == "")
            {
                return Content(HttpStatusCode.NotFound, "");
            }
            Guest guest = db.Guests.FirstOrDefault(x => x.UserID == userID);
            if (guest == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }
            Image img = db.Images.FirstOrDefault(x => x.HostObjID == guest.GuestID);
            if (img == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }
            guest.ImgSrc = img.ImageSrc;

            return Ok(guest);
        }

        // PUT: api/Guests
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuest( Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var guestInDb = db.Guests.FirstOrDefault(x => x.UserID == guest.UserID);
            guest.ID = guestInDb.ID;
            guest.GuestID = guestInDb.GuestID;
            guest.CreatedDate = guestInDb.CreatedDate;
            db.Entry(guestInDb).CurrentValues.SetValues(guest);
            var userInDb = new UserInSystem();
            try
            {
                db.SaveChanges();

                userInDb= db.UserInSystems.FirstOrDefault(x => x.UserID == guest.UserID);
                if(Convert.ToInt32(userInDb.PermissionID)==0)
                {
                    userInDb.PermissionID = "1";
                    db.Entry(userInDb).CurrentValues.SetValues(userInDb);
                }
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(guest.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            userInDb = db.UserInSystems.FirstOrDefault(x => x.UserID == guest.UserID);

            string strResponseMess = userInDb.PermissionID;
            strResponseMess += ";"+ guest.GuestName;
            return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent
                (
                    strResponseMess,
                    Encoding.UTF8,
                    "text/html"
                )
            });
        }

        // POST: api/Guests
        [ResponseType(typeof(Guest))]
        public IHttpActionResult PostGuest(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = guest.ID }, guest);
        }

        // DELETE: api/Guests/5
        [ResponseType(typeof(Guest))]
        public IHttpActionResult DeleteGuest(int id)
        {
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return NotFound();
            }

            db.Guests.Remove(guest);
            db.SaveChanges();

            return Ok(guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(int id)
        {
            return db.Guests.Count(e => e.ID == id) > 0;
        }

    }
}