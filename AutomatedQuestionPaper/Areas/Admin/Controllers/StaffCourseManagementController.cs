using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;
using AutomatedQuestionPaper.Areas.Staff.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class StaffCourseManagementController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Admin/StaffCourseManagement
        public ActionResult Index()
        {
            var T1 = new Thread(() =>
            {
                ViewBag.SemesterList = new SelectList(_context.Semesters, "SemesterName", "SemesterName");
            });
            T1.Start();

            var T2 = new Thread(() =>
            {
                ViewBag.StaffMembersList = new SelectList(_context.Staffs, "Name", "Name");
            });
            T2.Start();

            var T3 = new Thread(() =>
            {
                ViewBag.DepartmentList = new SelectList(_context.Departments, "DepartmentName", "DepartmentName");
            });
            T3.Start();

            var T4 = new Thread(() =>
            {
                ViewBag.Subject = new SelectList(_context.Courses, "CourseName", "CourseName");
            });
            T4.Start();

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

            var staffId = 0;

            // Get a selected Semester Id
            var semesterId = DatabaseData.GetSemesterInfo(selectedSemester).Id;

            // Get a selected staff Id
            var staff = _context.Staffs.FirstOrDefault(u => u.Name == selectedStaff);
            if (staff != null) staffId = staff.Id;

            // Get a selected department Id
            var departmentId = DatabaseData.GetDepartmentInfo(selectedDepartment).Id;

            // Get a selected subject id
            var subjectId = DatabaseData.GetCourseInfo(selectedSubject).Courseid;


            var newAllocatedCourse = new StaffCourse
            {
                CourseId = subjectId,
                SemesterId = semesterId,
                StaffId = staffId,
                DepartmentId = departmentId
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