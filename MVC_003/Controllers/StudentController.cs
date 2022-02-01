using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_003.DataAccess;

using MVC_003.Models;   // Models altındaki sınıfları kullanacağız.

namespace MVC_003.Controllers
{
    public class StudentController : Controller
    {

        //public ActionResult Index_Old()
        //{   // model yolu ile View'e veri gönderme

        //    Student stdnt = new Student()
        //    {
        //        ID = 99,
        //        Name = "Serdar",
        //        Surname = "Karakurt"
        //    };

        //    return View(stdnt);// /Views/Student/Index.cshtml'i çalıştır.
        //}
        public ActionResult Index()
        {
            List<Student> students = StudentDAL.Methods.List();

            return View(students);
        }

        public ActionResult About()
        {
            return RedirectToAction("Index"); // Yukarıdaki Index controller'ına yönlendir.
        }


        //Yeni öğrenci kayıt formunnu göster
        public ActionResult Add()
        {
            Student newStudent = new Student();

            return View(newStudent);
        }


        [HttpPost]
        public ActionResult Add(Student student)
        {
            TempData["insertedID"]=StudentDAL.Methods.Add(student);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(StudentDAL.Methods.GetByID(id));
        }
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            
            int affectedRows = (int)StudentDAL.Methods.Update(student);
            if (affectedRows>0)
            {
                TempData["editmessage"] = "Edit Successful";
            }
            else
            {
                TempData["editmessage"] = "Edit not Successful";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(StudentDAL.Methods.GetByID(id));
        }
        public ActionResult Delete(int id)
        {
            return View(StudentDAL.Methods.GetByID(id));
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            int affectedRows = (int)StudentDAL.Methods.Delete(student.ID);
            if (affectedRows > 0)
            {
                TempData["deletemessage"] = "Delete Successful";
            }
            else
            {
                TempData["deletemessage"] = "Delete not Successful";
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(string searchterm)
        {
            List<Student> searchedStudents = StudentDAL.Methods.Search(searchterm);
            return View(searchedStudents);
        }
    }
}