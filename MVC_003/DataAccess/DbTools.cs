using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MVC_003.Models;

namespace MVC_003.DataAccess
{
    public class DbTools
    {
        private static DbTools _Methods { get; set; }
        public static DbTools Methods 
        {
            get
            {
                if (_Methods == null)
                {
                    _Methods = new DbTools();
                }
                return _Methods;
            }
        }


        static string strConnnection = ConfigurationManager.ConnectionStrings["schoolDb"].ConnectionString;
        public static SqlConnection dbConnection = new SqlConnection(strConnnection);

        public bool ConnectDB()
        {
            try
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }


        public bool DisconnectDB()
        {
            try
            {
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public object Add(SqlCommand cmd)
        {
            object insertedID = -1;
            try
            {
                if (ConnectDB())
                {
                    insertedID = cmd.ExecuteScalar();
                    DisconnectDB();
                }
            }
            catch
            {
                return insertedID;
            }
            return insertedID;
        }




        public int Execute(SqlCommand cmd)
        {
            int affectedRows = 0;
            try
            {
                if (ConnectDB())
                {
                    affectedRows = cmd.ExecuteNonQuery();
                    DisconnectDB();
                }
            }
            catch
            {
                throw;
            }
            return affectedRows;
        }

        //public object Delete(SqlCommand cmd)
        //{
        //    object insertedID = -1;
        //    try
        //    {
        //        if (ConnectDB())
        //        {
        //            insertedID = cmd.ExecuteScalar();
        //            DisconnectDB();
        //        }
        //    }
        //    catch
        //    {
        //        return insertedID;
        //    }
        //    return insertedID;
        //}
    }
}