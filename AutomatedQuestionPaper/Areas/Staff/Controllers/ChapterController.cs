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
        private int? _semId, _deptId, _subId;



        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Staff/Chapter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChapter(string selectedSemester, string selectedDepartment, string selectedSubject, string selectedUnit, string chapterNumber, string chapterName)
        {
            // Get the Id of selected subject
            var subjectId = _context.Courses.FirstOrDefault(u => u.CourseName == selectedSubject)?.Courseid;


            // Get the Id of selected department 
            var departmentId = _context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment)?.Id;

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
            var name = (string)Session["Staff_Name"];

            // Get the logged staff Id
            var staffId = _context.Staffs.FirstOrDefault(u => u.Name == name)?.Id;


            // Get the semester Id
            var semID = _context.Semesters.FirstOrDefault(u => u.SemesterName == semesterName)?.Id;

            var departmentIDs = _context.StaffCourses.Where(u => u.SemesterId == semID && u.StaffId == staffId).Select(u => u.DepartmentId).Distinct().ToList();

            var departmentName = new List<string>();

            foreach (var id in departmentIDs)
            {
                var deptName = _context.Departments.FirstOrDefault(u => u.Id == id)?.DepartmentName;
                departmentName.Add(deptName);
            }

            return Json(departmentName, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubjectsList(string departmentName, string semesterName)
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

            TempData["Data_SubjectId"] = subjectId;

            // Get the Id of selected department 
            var departmentId = _context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment)?.Id;

            TempData["Data_DepartmentId"] = departmentId;

            // Get the Id of selected semester 
            var semesterId = _context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester)?.Id;

            TempData["Data_SemesterId"] = semesterId;

            var data = _context.Chapters.Where(u =>
                   u.SemesterId == semesterId && u.DepartmentId == departmentId && u.CourseId == subjectId)
                .Select(x => new { x.UnitNo, x.ChapterNo, x.ChapterName }).ToList();

            var list = data.Select(x => new ChapterDetails
            {
                UnitNo = x.UnitNo,
                ChapterName = x.ChapterName,
                ChapterNo = x.ChapterNo
            }).OrderBy(x => x.UnitNo).ToList();


            TempData["ListOfChapters"] = list;


            return View("ViewChapters", list);
        }

        [HttpGet]
        public ActionResult EditChapterDetails(string name)
        {
            var chapterId = _context.Chapters.FirstOrDefault(u => u.ChapterName == name)?.Id;

            var chapterInfo = _context.Chapters.FirstOrDefault(u => u.Id == chapterId);

            var semName = _context.Semesters.FirstOrDefault(u => u.Id == chapterInfo.SemesterId);

            var depName = _context.Departments.FirstOrDefault(u => u.Id == chapterInfo.DepartmentId);

            TempData["SemName"] = semName.SemesterName;
            TempData["depName"] = depName.DepartmentName;
            TempData["UnitNo"] = chapterInfo.UnitNo;

            return View(chapterInfo);
        }

        [HttpPost]
        public ActionResult EditChapterDetails(Chapter chap, string selectedSemester, string selectedDepartment, string selectedUnit)
        {
            // Get a corresponding chapter from db
            var dbChapter = _context.Chapters.FirstOrDefault(u=>u.Id == chap.Id);

            // Get the semester Id
            var semesterId = _context.Semesters.FirstOrDefault(u=>u.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(u => u.DepartmentName== selectedDepartment)?.Id;

            chap.DepartmentId = departmentId;
            chap.SemesterId = semesterId;
            chap.UnitNo = Convert.ToInt32(selectedUnit);
            
            dbChapter.ChapterName = chap.ChapterName;
            dbChapter.DepartmentId = chap.DepartmentId;
            dbChapter.SemesterId = chap.SemesterId;
            dbChapter.UnitNo = chap.UnitNo;
            dbChapter.ChapterNo = chap.ChapterNo;
            _context.SaveChangesAsync();


            TempData["ChapterDetailsEditedSuccessfully"] = "Chapter details edited successfully";

            return RedirectToAction("ViewChapters");
        }



        public ActionResult DeleteChapter(string name)
        {
            var subjectId = (int?)TempData["Data_SubjectId"];
            var departmentId = (int?)TempData["Data_DepartmentId"];
            var semesterId = (int?) TempData["Data_SemesterId"];

            var subject = _context.Chapters.FirstOrDefault(x =>
                x.ChapterName == name && x.SemesterId == semesterId && x.DepartmentId == departmentId &&
                x.CourseId == subjectId);
            if (subject != null)
            {
                _context.Chapters.Remove(subject);
                _context.SaveChangesAsync();

                TempData["ChapterDetailsDeletedSuccessfully"] = "Chapter deleted successfully";

                return RedirectToAction("ViewChapters");
            }

            return RedirectToAction("ViewChapters");
        }
    }
}