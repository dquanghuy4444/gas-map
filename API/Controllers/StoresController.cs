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
    public class StoresController : ApiController
    {
        private MVC_Gas_MapEntities db = new MVC_Gas_MapEntities();

        // GET: api/Stores
        public IEnumerable<Store> GetStores()
        {
            var view = RunDataUseProcedure.getStoreInfor("");
            return view;
        }

        // GET: api/AmountOfStores
        //public HttpResponseMessage GetAmountOfStores()
        //{
        //    System.Threading.Thread.Sleep(10000);
        //    string result = RunDataUseProcedure.getAmountOfStoresForCardInFormIndex();
        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent(
        //             result,
        //             Encoding.UTF8,
        //             "text/html"
        //         )
        //    };
        //}

        //GET: api/Stores?storeid=sto0001
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStore(string storeid)
        {
            var view = RunDataUseProcedure.getStoreInfor(storeid);
            if (view == null)
            {
                return NotFound();
            }

            return Ok(view);
        }

        // GET: api/Stores/5
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStore(int id, Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.ID)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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

        // POST: api/Stores
        [ResponseType(typeof(Store))]
        public IHttpActionResult PostStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.ID }, store);
        }

        // DELETE: api/Stores/5
        [ResponseType(typeof(Store))]
        public IHttpActionResult DeleteStore(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.Stores.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.ID == id) > 0;
        }
    }
}