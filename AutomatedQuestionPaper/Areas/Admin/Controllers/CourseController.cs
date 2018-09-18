using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private readonly ModelContainer _context = new ModelContainer();
        private readonly DbSet<Course> _data;

        public CourseController() : this(1) { }

        public CourseController(int sdata)
        {
            _data = _context.Courses;
        }

        public ActionResult Index()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();


            return View(_data.ToList());
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }
    }
}