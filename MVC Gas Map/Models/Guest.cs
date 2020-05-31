using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Gas_Map.Models
{
    public class Guest
    {
        public string GuestID { get; set; }
        public string GuestName { get; set; }
        public string GuestPhone { get; set; }
        public string GuestEmail { get; set; }
        public Nullable<int> GuestSex { get; set; }
        public string GuestAddress { get; set; }
        public string ImgSrc { get; set; }
        public Nullable<System.DateTime> GuestBirthday { get; set; }
        public string UserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}