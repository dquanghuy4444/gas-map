using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Gas_Map.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string PermissionID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}