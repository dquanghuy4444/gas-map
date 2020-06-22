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
        [ResponseType(typeof(List<Store>))]
        public IHttpActionResult GetStores()
        {
            //var view = RunDataUseProcedure.getStoreInfor("");
            var view = from store in db.Stores
                       join coord in db.Coordinates on store.StoreID equals coord.HostObjID
                       join img in db.Images on store.StoreID equals img.HostObjID
                       select new
                       {
                           StoreName = store.StoreName,
                           StoreID = store.StoreID,
                           Latitude = coord.Latitude,
                           Longitude = coord.Longitude,
                           ImgSrc = img.ImageSrc
                       };
            return Json(view);
        }

        // GET: api/Stores
        [ResponseType(typeof(Store))]
        public IHttpActionResult GetStores(string userID)
        {
            var storeList = db.Stores.Where(x => x.UserID == userID).ToList();

            for(int i=0;i<storeList.Count;i++)
            {
                var storeID = storeList[i].StoreID;
                var coord = db.Coordinates.FirstOrDefault(x => x.HostObjID == storeID);
                if(coord !=null)
                {
                    storeList[i].Latitude = coord.Latitude;
                    storeList[i].Longitude = coord.Longitude;
                }
                var img = db.Images.FirstOrDefault(x => x.HostObjID == storeID);
                if (img != null)
                {
                    storeList[i].ImgSrc = img.ImageSrc;
                }
            }
            return Json(storeList);
        }

        // GET : api/Stores?mode=0
        [ResponseType(typeof(string))]
        public IHttpActionResult GetStore(int mode)
        {
            string result = null;
            if(mode==1)
                result = db.Stores.Count().ToString();
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
            //var store = RunDataUseProcedure.getStoreInfor(storeid);
            var view = from store in db.Stores
                       join coord in db.Coordinates on store.StoreID equals coord.HostObjID
                       join img in db.Images on store.StoreID equals img.HostObjID
                       where store.StoreID== storeid
                       select new
                       {
                           StoreName = store.StoreName,
                           StoreID = store.StoreID,
                           Latitude = coord.Latitude,
                           Longitude = coord.Longitude,
                           ImgSrc = img.ImageSrc
                       };
            if (view == null)
            {
                return NotFound();
            }

            return Json(view);
        }


        // PUT: api/Stores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStore(Store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storeInDB = db.Stores.FirstOrDefault(x => x.StoreName.ToLower() == store.StoreName.ToLower());
            if (storeInDB != null && storeInDB.StoreID != store.StoreID)
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

            storeInDB = db.Stores.FirstOrDefault(x => x.StoreID == store.StoreID);
            if (storeInDB == null)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Không tìm thấy thông tin trong cơ sở dữ liệu",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }
            storeInDB.StoreName = store.StoreName;
            storeInDB.StoreEmail = store.StoreEmail;
            storeInDB.StorePhone = store.StorePhone;
            storeInDB.StoreDetail = store.StoreDetail;
            storeInDB.StoreAddress = store.StoreAddress;

            db.Entry(storeInDB).CurrentValues.SetValues(storeInDB);

            var coordInDB = db.Coordinates.FirstOrDefault(x => x.HostObjID == store.StoreID);
            if (coordInDB == null)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Không tìm thấy thông tin trong cơ sở dữ liệu",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }
            coordInDB.Latitude = store.Latitude;
            coordInDB.Longitude = store.Longitude;

            db.Entry(coordInDB).CurrentValues.SetValues(coordInDB);

            var imgInDB = db.Images.FirstOrDefault(x => x.HostObjID == store.StoreID);
            if (imgInDB == null)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Không tìm thấy thông tin trong cơ sở dữ liệu",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }
            imgInDB.ImageSrc = store.ImgSrc;

            db.Entry(imgInDB).CurrentValues.SetValues(imgInDB);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent
                    (
                        "Lỗi cập nhật",
                        Encoding.UTF8,
                        "text/html"
                    )
                });
            }

            return base.ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent
                (
                    "Cập nhật thông tin cửa hàng",
                    Encoding.UTF8,
                    "text/html"
                )
            });
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

            var userInDb= db.UserInSystems.FirstOrDefault(x => x.UserID==store.UserID);
            if(Convert.ToInt32(userInDb.PermissionID)==3)
                userInDb.PermissionID="4";
            db.Entry(userInDb).CurrentValues.SetValues(userInDb);
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