using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Gas_Map.Class
{
    public class ConstData
    {
        public enum FlagCheck:int
        {
            Fail=0,
            Success=1
        }

        public const string STR_IMAGE_STORE = "/Images/Store";
        public const string STR_IMAGE_GUEST = "/Images/Guest";

    }
}