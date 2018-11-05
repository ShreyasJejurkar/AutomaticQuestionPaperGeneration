﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class StaffCourseManagementController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Admin/StaffCourseManagement
        public ActionResult Index()
        {
            // Set them in ViewBag for the view
            ViewBag.SemesterList = new SelectList(_context.Semesters, "SemesterName", "SemesterName");
            ViewBag.StaffMembersList = new SelectList(_context.Staffs, "Name", "Name");
            ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentName", "DepartmentName");
            ViewBag.Subject = new SelectList(_context.Courses, "CourseName", "CourseName");
            return View();
        }

        public ActionResult GetDetails(string selectedSemester, string selectedStaff, string selectedDepartment,
            string selectedSubject)
        {
            if (string.IsNullOrEmpty(selectedSemester) || string.IsNullOrEmpty(selectedStaff) ||
                string.IsNullOrEmpty(selectedDepartment) || selectedSubject.Contains("---Select---"))
            {
                TempData["AllocatedErrorMessage"] = "Please fill all of the fields";

                return RedirectToAction("Index");
            }

            int semesterId = 0, staffId = 0, subjectId = 0, departmentID = 0;

            // Get a selected Semester Id
            var semester = _context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester);
            if (semester != null) semesterId = semester.Id;

            // Get a selected staff Id
            var staff = _context.Staffs.FirstOrDefault(u => u.Name == selectedStaff);
            if (staff != null) staffId = staff.Id;

            // Get a selected department Id
            var department = _context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment);
            if (department != null) departmentID = department.Id;

            // Get a selected subject id
            var subject = _context.Courses.FirstOrDefault(u => u.CourseName == selectedSubject);
            if (subject != null) subjectId = subject.Courseid;

            var newAllocatedCourse = new StaffCourse
            {
                CourseId = subjectId,
                SemesterId = semesterId,
                StaffId = staffId,
                DepartmentId = departmentID
            };

            _context.StaffCourses.Add(newAllocatedCourse);
            _context.SaveChanges();

            TempData["AllocatedSuccessMessage"] = "Subject allocated to staff successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetSubjectsOfDepartment(string departmentName)
        {
            var department = _context.Departments.FirstOrDefault(u => u.DepartmentName == departmentName);
            if (department != null)
            {
                var departmentCourses =
                    (from c in _context.Courses where c.DepartmentId == department.Id select c).ToList();


                var deptListItems = new List<string>();

                foreach (var dept in departmentCourses) deptListItems.Add(dept.CourseName);

                return Json(deptListItems, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}