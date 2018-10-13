﻿using System;
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

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentList = _context.Departments.ToList();

            // Engineering year list
            var yearsList = new List<string>
            {
                "Second year",
                "Third year",
                "Fourth year"
            };

            ViewBag.YearsList = yearsList;

            return View();
        }

        /// <summary>
        /// Action for creating course
        /// </summary>
        /// <param name="c">Course details</param>
        /// <param name="DepartmentList">Selected department</param>
        /// <param name="YearList">Selected year</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Course c, string DepartmentList, string YearList)
        {
            // Get te ID of department which is selected by user
            var result = _context.Departments.FirstOrDefault(p => p.DepartmentName == DepartmentList);

            //Set the remaining field of course object
            c.DepartmentId = result.Id;  //department ID
            c.Year = YearList;           // year ID

            //Save it do database
            _context.Courses.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
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
            //Check for user has selected a department or not
            if (DepartmentList.Contains("department"))
            {
                // Get the department information from database
                var department = _context.Departments.FirstOrDefault(u => u.DepartmentName == DepartmentList);
                var departmentId = department.Id;

                // Get the list of subjects of selected department 
                var listOfCourses = _context.Courses.Where(u => u.DepartmentId == departmentId).ToList();
                TempData["CoursesList"] = listOfCourses;

                return RedirectToAction("Index", "Course");
            }

            //In case user didn't selected any department 
            TempData["DepartmentNotSelectedErrorMessage"] = "Please select department first";
            return RedirectToAction("Index", "Course");
        }

        public ActionResult GetSubjectDetails(string SubjectCode)
        {
            ViewBag.DepartmentList = _context.Departments.ToList();
            
            //Get the subjects details as per subject code  
            var subject = _context.Courses.FirstOrDefault(u => u.CourseCode == SubjectCode);

            //Pass it to view
            if (subject != null)
            {
                return View("Edit", subject);
            }
            else
            {
                TempData["SubjectNotFoundErrorMessage"] = "Incorrect subject code.";
                return View("Edit", null);
            }
        }
    }
}