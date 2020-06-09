using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Gas_Map.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StorePhone { get; set; }
        public string StoreEmail { get; set; }
        public string StoreDetail { get; set; }
        public string UserID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ImgSrc { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}