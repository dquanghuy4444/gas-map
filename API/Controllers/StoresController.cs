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
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStores()
        {
            var view = RunDataUseProcedure.getStoreInfor("");
            return Json(view);
        }

        // GET : api/Stores?mode=0
        [ResponseType(typeof(string))]
        public IHttpActionResult GetStore(int mode)
        {
            string result = null;
            if(mode==1)
                result = db.Stores.Count().ToString();
            else if(mode ==2)
                result = db.UserInSystems.Count().ToString();

            return base.ResponseMessage(new HttpResponseMessage()
            {
                Content = new StringContent(
                    result,
                    Encoding.UTF8,
                    "text/html"
                )
            });
        }

        // GET : api/Stores?storeid=sto0001
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStore(string storeid)
        {
            var view = RunDataUseProcedure.getStoreInfor(storeid);
            if (view == null)
            {
                return NotFound();
            }

            return Json(view);
        }

        //// GET: api/Stores/5
        //[ResponseType(typeof(Store))]
        //public IHttpActionResult GetStore(int id)
        //{
        //    Store store = db.Stores.Find(id);
        //    if (store == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(store);
        //}

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
            var storeIndb = new Store();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            storeIndb = db.Stores.FirstOrDefault(x => x.StoreName.ToLower() == store.StoreName.ToLower());
            if(storeIndb !=null)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Tên cửa hàng đã được sử dụng.Hãy đặt tên khác ",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }

            var amountOfStoresOfUserID = db.Stores.Count(x => x.UserID == store.UserID);
            if(amountOfStoresOfUserID>5)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Bạn đã tạo quá giới hạn 5 cửa hàng .Không thể thêm được nữa",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }

            db.Stores.Add(store);
            db.SaveChanges();

            storeIndb = db.Stores.FirstOrDefault(x => x.StoreName.ToLower() == store.StoreName.ToLower());

            var coordinate = new Coordinate();
            coordinate.HostObjID = storeIndb.StoreID;
            coordinate.Latitude = storeIndb.Latitude;
            coordinate.Longitude = storeIndb.Longitude;

            db.Coordinates.Add(coordinate);
            db.SaveChanges();

            var image = new Image();
            image.HostObjID = storeIndb.StoreID;
            image.ImageSrc = store.ImgSrc;

            db.Images.Add(image);
            db.SaveChanges();

            return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent
                (
                    "Bạn đã tạo cửa hàng thành công.",
                    Encoding.UTF8,
                    "text/html"
                )
            });
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