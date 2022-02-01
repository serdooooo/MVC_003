using MVC_003.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_003.DataAccess
{
    public class TeacherDAL
    {
        private static TeacherDAL _Methods { get; set; }
        public static TeacherDAL Methods
        {
            get
            {
                if (_Methods == null)
                {
                    _Methods = new TeacherDAL();
                }
                return _Methods;
            }
        }
        public object Add(Teacher teacher)
        {
            string query = $"INSERT INTO Teacher (Name, Surname) VALUES ('{teacher.Name}','{teacher.Surname}') SELECT SCOPE_IDENTITY() ;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@name", teacher.Name);
            cmd.Parameters.AddWithValue("@surname", teacher.Surname);
            return DbTools.Methods.Add(cmd);
        }

        public List<Teacher> List()
        {
            string query = $"SELECT * FROM Teacher;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            List<Teacher> teacherList = new List<Teacher>();
            IDataReader reader;
            try
            {
                DbTools.Methods.ConnectDB();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teacherList.Add(
                        new Teacher()
                        {
                            ID = reader.GetInt32(0),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString()
                        }
                    );
                }
                DbTools.Methods.DisconnectDB();
            }
            catch
            {

            }
            return teacherList;
        }
        //public object Delete(int id)
        //{
        //    string sql = $"DELETE FROM Teacher WHERE Id={id};";
        //    SqlCommand cmd = new SqlCommand(sql, DbTools.dbConnection);
        //    {

        //        DbTools.dbConnection.Open();
        //        cmd.ExecuteNonQuery();
        //        DbTools.dbConnection.Close();
        //    }
        //    return DbTools.Methods.Delete(cmd);
        //}
    }
}