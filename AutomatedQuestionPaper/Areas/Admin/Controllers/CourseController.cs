﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class CourseController : BaseController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

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
        ///     Action for creating course
        /// </summary>
        /// <param name="c">Course details</param>
        /// <param name="departmentList">Selected department</param>
        /// <param name="yearList">Selected year</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course c, string departmentList, string yearList)
        {
            // Get the ID of department which is selected by user
            var result = _context.Departments.FirstOrDefault(p => p.DepartmentName == departmentList);

            if (result != null)
            {
                // Set the remaining field of course object
                c.DepartmentId = result.Id;
                c.Year = yearList;
            }

            // Save it do database
            _context.Courses.Add(c);
            _context.SaveChanges();



            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            // Get subject from database
            var subject = _context.Courses.FirstOrDefault(u => u.Courseid == id);
            if (subject != null)
            {
                _context.Courses.Remove(subject);
                _context.SaveChanges();

                // Set the success message 
                TempData["SubjectedDeletedSuccessfully"] = "Subject deleted successfully";

                return RedirectToAction("Index", "Course");
            }

            TempData["SubjectedFailedSuccessfully"] = "Subject deletion failed";

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dbCourse = _context.Courses.FirstOrDefault(u => u.Courseid == id);
            return View(dbCourse);
        }

        [HttpPost]
        public ActionResult Edit(Course editedCourse)
        {
            //Find the course from database first
            var dbCourse = _context.Courses.FirstOrDefault(u => u.Courseid == editedCourse.Courseid);

            //Setting up individual properties
            if (dbCourse != null)
            {
                dbCourse.DepartmentId = editedCourse.DepartmentId;
                dbCourse.CourseCode = editedCourse.CourseCode;
                dbCourse.CourseName = editedCourse.CourseName;
                dbCourse.Description = editedCourse.Description;
                dbCourse.Year = editedCourse.Year;

                //Commit it to database
                _context.SaveChanges();

                //Set success message
                TempData["SubjectedEditedSuccessfully"] = "Subject detail edited successfully";

                return RedirectToAction("Index");
            }

            TempData["SubjectedEditedFailure"] = "Subjects details editing failed";
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     Will return the list of subjects
        /// </summary>
        /// <param name="departmentList">Contains the selection of the department</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSubjects(string departmentList)
        {
            // Check for user has selected a department or not
            if (departmentList.Contains("department"))
            {
                // Get the department information from database
                var department = _context.Departments.FirstOrDefault(u => u.DepartmentName == departmentList);

                if (department != null)
                {
                    var departmentId = department.Id;

                    // Get the list of subjects of selected department 
                    var listOfCourses = _context.Courses.Where(u => u.DepartmentId == departmentId).ToList();

                    TempData["CoursesList"] = listOfCourses;
                    return View("Index", listOfCourses);
                }
            }

            // In case user didn't selected any department 
            TempData["DepartmentNotSelectedErrorMessage"] = "Please select department first";
            return RedirectToAction("Index", "Course");
        }

        public ActionResult GetSubjectDetails(string subjectCode)
        {
            ViewBag.DepartmentList = _context.Departments.ToList();

            // Convert subject code to int
            var code = Convert.ToInt32(subjectCode);

            // Get the subjects details as per subject code  
            var subject = _context.Courses.FirstOrDefault(u => u.Courseid == code);

            // Pass it to view
            if (subject != null)
            {
                TempData["subjectSelectedYear"] = subject.Year;

                return View("Edit", subject);
            }

            TempData["SubjectNotFoundErrorMessage"] = "Incorrect subject code.";
            return View("Edit", null);
        }
    }
}