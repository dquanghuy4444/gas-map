using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Gas_Map.Models
{
    public class RequestFromGuest
    {
        public int ID { get; set; }
        public string ReqFrmGueID { get; set; }
        public string GuestID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GuestName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}