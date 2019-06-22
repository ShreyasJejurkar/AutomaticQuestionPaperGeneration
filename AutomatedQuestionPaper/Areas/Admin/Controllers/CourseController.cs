using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutomatedQuestionPaper.ApplicationLogic;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.DataAccessLayer;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class CourseController : AlertController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CourseController()
        {
            _courseRepository = new CourseRepository();
            _departmentRepository = new DepartmentRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartment();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartment();

            ViewBag.YearsList = AcademicData.GetListOfYears();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course c, string departmentList, string yearList)
        {
            var result = _departmentRepository.GetDepartmentByName(departmentList);

            if (result != null)
            {
                c.DepartmentId = result.Id;
                c.Year = yearList;
            }

            _departmentRepository.AddDepartment(result);

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _courseRepository.DeleteCourse(id);

            Alert("Success", "Subject deleted successfully", Enums.NotificationType.success);

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dbCourse = _courseRepository.GetCoursesById(id);
            return View(dbCourse);
        }

        [HttpPost]
        public ActionResult Edit(Course editedCourse)
        {
            _courseRepository.UpdateCourse(editedCourse.Courseid, editedCourse);

            Alert("Success", "Subject detail edited successfully", Enums.NotificationType.success);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetSubjects(string departmentList)
        {
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartment();

            if (departmentList.Contains("department"))
            {
                var department = _departmentRepository.GetDepartmentByName(departmentList);

                if (department != null)
                {
                    var listOfCourses = _courseRepository.GetAllCoursesByDepartment(department);

                    TempData["CoursesList"] = listOfCourses;
                    return View("Index", listOfCourses);
                }
            }

            Alert("Warning", "Please select department first", Enums.NotificationType.warning);

            return RedirectToAction("Index", "Course");
        }

        [HttpGet]
        public ActionResult GetSubjectDetails(string subjectCode)
        {
            ViewBag.DepartmentList = _departmentRepository.GetAllDepartment();

            var code = Convert.ToInt32(subjectCode);
            var subject = _courseRepository.GetCoursesById(code);

            if (subject != null)
            {
                TempData["subjectSelectedYear"] = subject.Year;

                return View("Edit", subject);
            }

            Alert("Warning", "Incorrect subject code", Enums.NotificationType.warning);
            return View("Edit", null);
        }

        [HttpPost]
        public ActionResult DeleteMultipleSubjects(IEnumerable<int> selectedIds)
        {
            if (selectedIds == null)
            {
                Alert("Error", "Select at least one subject record to delete", Enums.NotificationType.warning);
                return RedirectToAction("Index");
            }

            else
            {
                foreach (var id in selectedIds)
                {
                    _courseRepository.DeleteCourse(id);
                }

                Alert("Success", "Selected subjects records deleted", Enums.NotificationType.success);
                return RedirectToAction("Index");
            }
        }
    }
}