using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Course> _data;

        public CourseController() : this(1) { }

        public CourseController(int sdata)
        {
            _data = _context.Courses;
        }

        public ActionResult Index()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();

            var yearsList = new List<string>
            {
                "Second year",
                "Third year",
                "Fourth year"
            };

            ViewBag.YearsList = yearsList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Course c, string DepartmentList, string YearList)
        {
            var result = _context.Departments.FirstOrDefault(p => p.DepartmentName == DepartmentList);
            c.DepartmentId = result.Id;
            c.Year = YearList;
            _context.Courses.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Index", "Course");
        }


        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        
        /// <summary>
        /// Will return the list of subjects
        /// </summary>
        /// <param name="DepartmentList">Contains the selection of the department</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSubjects(string DepartmentList)
        {
            if (DepartmentList.Contains("department"))
            {
                var department = _context.Departments.FirstOrDefault(u => u.DepartmentName == DepartmentList);
                var departmentId = department.Id;

                var listOfCourses = _context.Courses.Where(u => u.DepartmentId == departmentId).ToList();
                TempData["CoursesList"] = listOfCourses;
                return RedirectToAction("Index", "Course");
            }

            TempData["DepartmentNotSelectedErrorMessage"] = "Please select department first";
            return RedirectToAction("Index", "Course");
        }
    }
}