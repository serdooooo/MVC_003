﻿using MVC_003.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVC_003.DataAccess
{
    public class StudentDAL
    {
        private static StudentDAL _Methods { get; set; }
        public static StudentDAL Methods
        {
            get
            {
                if (_Methods==null)
                {
                    _Methods = new StudentDAL();
                }
                return _Methods;
            }
        }
        public object Add(Student student)
        {
            string query = $"INSERT INTO Student (Name,Surname) VALUES ('{student.Name}', '{student.Surname}') SELECT SCOPE_IDENTITY() ;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@surname", student.Surname);
            return DbTools.Methods.Add(cmd);
        }



        public List<Student> List()
        {
            string query = $"SELECT * FROM Student;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            return GetStudentList(cmd);
        }

        private static List<Student> GetStudentList(SqlCommand cmd)
        {
            List<Student> studentList = new List<Student>();
            IDataReader reader;
            try
            {
                DbTools.Methods.ConnectDB();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentList.Add(
                    new Student()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString()
                    });
                }
                DbTools.Methods.DisconnectDB();
            }
            catch
            {
                throw;
            }
            return studentList;
        }


        public Student GetByID(int id)
        {
            string query = $"SELECT * FROM Student WHERE Id={id};";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                return GetStudentList(cmd)[0];
            }
            catch (Exception)
            {

                throw;
            }
        }


        public object Update(Student student)
        {
            string query = $"Update Student SET Name=@name,Surname=@surname WHERE Id=@id ;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@surname", student.Surname);
            cmd.Parameters.AddWithValue("@id", student.ID);
            return DbTools.Methods.Execute(cmd);
            
        }

        public object Delete(int id)
        {
            string query = $"Delete From Student WHERE Id=@id ;";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            cmd.Parameters.AddWithValue("@id", id);
            return DbTools.Methods.Execute(cmd);

        }


        public List<Student> Search(string searchterm)
        {
            string query = $"SELECT * FROM Student WHERE Name LIKE '%{searchterm}%' OR Surname LIKE '%{searchterm}%';";
            SqlCommand cmd = new SqlCommand(query, DbTools.dbConnection);
            try
            {
                return GetStudentList(cmd);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}