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
    public class RequestFromGuestsController : ApiController
    {
        private MVC_Gas_MapEntities db = new MVC_Gas_MapEntities();

        [ResponseType(typeof(List<RequestFromGuest>))]
        public IHttpActionResult GetRequestFromGuests()
        {
            var view = db.RequestFromGuests.ToList();
            for(int i=0;i<view.Count();i++)
            {
                var req = view[i];
                string guestName = db.Guests.Where(x => x.GuestID == req.GuestID).FirstOrDefault().GuestName;
                view[i].GuestName = guestName;
            }
            return Json(view);
        }

        // GET: api/RequestFromGuests/5
        [ResponseType(typeof(RequestFromGuest))]
        public IHttpActionResult GetRequestFromGuest(string reqFrmGueID)
        {
            RequestFromGuest requestFromGuest = db.RequestFromGuests.Where(x=>x.ReqFrmGueID== reqFrmGueID).FirstOrDefault();
            if (requestFromGuest == null)
            {
                return NotFound();
            }

            return Ok(requestFromGuest);
        }

        // PUT: api/RequestFromGuests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequestFromGuest(int id, RequestFromGuest requestFromGuest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestFromGuest.ID)
            {
                return BadRequest();
            }

            db.Entry(requestFromGuest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestFromGuestExists(id))
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

        // POST: api/RequestFromGuests
        [ResponseType(typeof(RequestFromGuest))]
        public IHttpActionResult PostRequestFromGuest(RequestFromGuest requestFromGuest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (requestFromGuest == null)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Tạo yêu cầu thất bại",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }

            requestFromGuest.GuestID = db.Guests.Where(x => x.UserID == requestFromGuest.UserID).FirstOrDefault().GuestID;

            db.RequestFromGuests.Add(requestFromGuest);
            db.SaveChanges();

            return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent
                (
                    "Tạo yêu cầu thành công",
                    Encoding.UTF8,
                    "text/html"
                )
            });
        }

        // DELETE: api/RequestFromGuests/5
        [ResponseType(typeof(RequestFromGuest))]
        public IHttpActionResult DeleteRequestFromGuest(int id)
        {
            RequestFromGuest requestFromGuest = db.RequestFromGuests.Find(id);
            if (requestFromGuest == null)
            {
                return NotFound();
            }

            db.RequestFromGuests.Remove(requestFromGuest);
            db.SaveChanges();

            return Ok(requestFromGuest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestFromGuestExists(int id)
        {
            return db.RequestFromGuests.Count(e => e.ID == id) > 0;
        }
    }
}