using MVC_003.DataAccess;
using MVC_003.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_003.Controllers
{
    public class TeacherController : Controller
    {
        List<Teacher> teachers = TeacherDAL.Methods.List();
        
        // GET: Teacher
        public ActionResult Index()
        {
            Teacher tchr = new Teacher();
            return View(tchr);
        }
        public ActionResult List()
        {
            

            return View(teachers);
        }
        public ActionResult Add()
        {
            Teacher newTeacher = new Teacher();
            return View(newTeacher);
        }
        [HttpPost]
        public ActionResult Add(Teacher teacher)
        {
            TempData["insertedID"] = TeacherDAL.Methods.Add(teacher);
            return RedirectToAction("Index");
        }

        //public ActionResult Delete()
        //{
        //    return View();
        //}

        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    teachers.RemoveAt(id);
        //    return View("Index");
        //}
    }
}