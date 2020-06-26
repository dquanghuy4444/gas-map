using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class RequestFromGuestsController : ApiController
    {
        private MVC_Gas_MapEntities db = new MVC_Gas_MapEntities();

        // GET: api/RequestFromGuests
        public IQueryable<RequestFromGuest> GetRequestFromGuests()
        {
            return db.RequestFromGuests;
        }

        // GET: api/RequestFromGuests/5
        [ResponseType(typeof(RequestFromGuest))]
        public IHttpActionResult GetRequestFromGuest(int id)
        {
            RequestFromGuest requestFromGuest = db.RequestFromGuests.Find(id);
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

            db.RequestFromGuests.Add(requestFromGuest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = requestFromGuest.ID }, requestFromGuest);
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