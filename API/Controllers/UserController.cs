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

        // GET: api/User/5
        [ResponseType(typeof(UserInSystem))]
        public IHttpActionResult GetUserInSystem(string username,string password)
        {
            if (username =="" || password=="")
            {
                return Content(HttpStatusCode.NotFound, ""); 
            }
            UserInSystem userInSystem = db.UserInSystems.FirstOrDefault(x=>x.UserName== username && x.UserName == password);
            if (userInSystem == null)
            {
                return Content(HttpStatusCode.Forbidden, "");
            }

                return Content(HttpStatusCode.Accepted, "");
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
            userInSystem.CreatedDate = DateTime.Now;
            db.UserInSystems.Add(userInSystem);
            db.SaveChanges();

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