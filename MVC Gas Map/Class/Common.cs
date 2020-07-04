using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MVC_Gas_Map.Class
{
    public class Common
    {
        //----------------HASH BY MD5
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        //----------------SAVE AS IMAGE
        public static void SaveAsImage(string mode,string HostObjID,string src)
        {
            var fileName = Path.GetFileName(src); 
            var extension = Path.GetExtension(src);

            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
            string myfile = name + "_" + tbl.Id + extension; //appending the name with id  
                                                       // store the file inside ~/project folder(Img)  
            var path = Path.Combine(Server.MapPath("~/Img"), myfile);

            file.SaveAs(path);
        }
    }
}