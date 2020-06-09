using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class RunDataUseProcedure
    {
        //private static string conStr = @"Data Source=DESKTOP-O7QF51K\POHIPPC;Initial Catalog=MVC Gas Map;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        private static string conStr = @"Data Source=POHIPPC;Initial Catalog=MVC Gas Map;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        private static SqlConnection sqlcon = new SqlConnection(conStr);

        public static List<Store> getStoreInfor(string storeid)
        {
            sqlcon.Open();
            List<Store> result = new List<Store>();
            SqlCommand com = new SqlCommand("getStoreInfor", sqlcon);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@storeid", storeid);
            SqlDataReader dataReader = com.ExecuteReader();
            while (dataReader.Read())
            {
                Store store = new Store();
                store.ID = Convert.ToInt32(dataReader["ID"]);
                store.StoreID = dataReader["StoreID"].ToString();
                store.StoreEmail = dataReader["StoreEmail"].ToString();
                store.StoreAddress = dataReader["StoreAddress"].ToString();
                store.StoreDetail = dataReader["StoreDetail"].ToString();
                store.StoreName = dataReader["StoreName"].ToString();
                store.StorePhone = dataReader["StorePhone"].ToString();
                store.Longitude = dataReader["Longitude"].ToString();
                store.Latitude = dataReader["Latitude"].ToString();
                store.UserID = dataReader["UserID"].ToString();
                store.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);
                result.Add(store);
            }
            sqlcon.Close();
            return result;
        }



    }
}