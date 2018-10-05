using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Cours> _data;

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

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();

            return View();
        }   

        [HttpPost]
        public ActionResult Create(Cours c, string DepartmentList)
        {
            ViewBag.DepartmentList = _context.Departments.ToList();

            return View();
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