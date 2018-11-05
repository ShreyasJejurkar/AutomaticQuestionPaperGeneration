using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class ChapterController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Staff/Chapter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChapter(string selectedSemester,string selectedDepartment, string selectedSubject, string selectedUnit, string chapterNumber, string chapterName)
        {
            // Get the Id of selected subject
            var subjectId = _context.Courses.FirstOrDefault(u=>u.CourseName == selectedSubject)?.Courseid;


            // Get the Id of selected department 
            var departmentId = _context.Departments.FirstOrDefault(u=>u.DepartmentName == selectedDepartment)?.Id;

            // Get the Id of selected semester 
            var semesterId = _context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester)?.Id;


            _context.Chapters.Add(new Chapter()
            {
                ChapterName = chapterName,
                ChapterNo = Convert.ToInt32(chapterNumber),
                CourseId = subjectId,
                UnitNo = Convert.ToInt32(selectedUnit),
                DepartmentId = departmentId,
                SemesterId = semesterId

            });

            _context.SaveChangesAsync();

            TempData["ChapterAddedSuccessMessage"] = "Chapter added successfully";
            return View("Index");
        }

        [HttpGet]
        public ActionResult GetAllocatedSemesters()
        {
            var staffName = (string)Session["Staff_Name"];
            var staffId = _context.Staffs.FirstOrDefault(u => u.Name == staffName)?.Id;

            var semesterIDs = _context.StaffCourses.Where(u => u.StaffId == staffId).Select(u => u.SemesterId).Distinct().ToList(); ;
           
            var semesterName = new List<string>();

            foreach (var id in semesterIDs)
            {
                var semesName = _context.Semesters.FirstOrDefault(u => u.Id == id)?.SemesterName;
                semesterName.Add(semesName);
            }

            return Json(semesterName, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetAllocatedDepartments(string semesterName)
        {
            // Get the semester Id
            var semID = _context.Semesters.FirstOrDefault(u=>u.SemesterName == semesterName)?.Id;
            
            var departmentIDs = _context.StaffCourses.Where(u => u.SemesterId == semID).Select(u => u.DepartmentId).Distinct().ToList();

            var departmentName = new List<string>();

            foreach (var id in departmentIDs)
            {
                var deptName = _context.Departments.FirstOrDefault(u => u.Id == id)?.DepartmentName;
                departmentName.Add(deptName);
            }

            return Json(departmentName,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubjectsList(string departmentName , string semesterName)
        {
            // Get the selected department Id
            var deptId = _context.Departments.FirstOrDefault(u => u.DepartmentName == departmentName)?.Id;
            
            // Get the selected sem Id
            var semId = _context.Semesters.FirstOrDefault(u => u.SemesterName == semesterName)?.Id;
            
            var subjectIDs = _context.StaffCourses.Where(u => u.DepartmentId == deptId && u.SemesterId == semId).Select(u => u.CourseId).Distinct().ToList();

            var subjectsName = new List<string>();

            foreach (var id in subjectIDs)
            {
                var subjectName = _context.Courses.FirstOrDefault(u => u.Courseid == id)?.CourseName;
                subjectsName.Add(subjectName);
            }

            return Json(subjectsName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewChapters()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetChapters(string selectedSemester, string selectedDepartment, string selectedSubject)
        {
            // Get the Id of selected subject
            var subjectId = _context.Courses.FirstOrDefault(u => u.CourseName == selectedSubject)?.Courseid;


            // Get the Id of selected department 
            var departmentId = _context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment)?.Id;

            // Get the Id of selected semester 
            var semesterId = _context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester)?.Id;
            
            var data =  _context.Chapters.Where(u =>
                    u.SemesterId == semesterId && u.DepartmentId == departmentId && u.CourseId == subjectId)
                .Select(x => new {x.UnitNo, x.ChapterNo, x.ChapterName}).ToList();
            
            var list = data.Select(x => new ChapterDetails
            {
                UnitNo = x.UnitNo,
                ChapterName = x.ChapterName,
                ChapterNo = x.ChapterNo
            }).OrderBy(x=>x.UnitNo).ToList();

            
            TempData["ListOfChapters"] = list;


            return View("ViewChapters",list);
        }
    }
}