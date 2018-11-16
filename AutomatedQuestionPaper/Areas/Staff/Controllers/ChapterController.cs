using System;
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
        private readonly DatabaseStaffOperations _db;

        public ChapterController()
        {
            var staffName = (string)System.Web.HttpContext.Current.Session["Staff_Name"];

            _db = new DatabaseStaffOperations(staffName);
        }

        // GET: Staff/Chapter
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddChapter(string selectedSemester, string selectedDepartment, string selectedSubject, string selectedUnit, string chapterNumber, string chapterName)
        {
            StaffChapterOperation.AddChapter(selectedSemester, selectedDepartment, selectedSubject, selectedUnit,
                chapterNumber, chapterName);

            TempData["ChapterAddedSuccessMessage"] = "Chapter added successfully";

            return View("Index");
        }

        [HttpGet]
        public ActionResult GetAllocatedSemesters()
        {
            var semesterName = _db.GetAllocatedSemesterNames();

            return Json(semesterName, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllocatedDepartments(string semesterName)
        {
            var departmentName = _db.GetAllocatedDepartment(semesterName);

            return Json(departmentName, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubjectsList(string departmentName, string semesterName)
        {
            var subjectsName = _db.GetAllocatedSubjects(semesterName, departmentName);

            return Json(subjectsName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewChapters()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetChapters(string selectedSemester, string selectedDepartment, string selectedSubject)
        {
            var list = _db.GetChapters(selectedSemester, selectedDepartment, selectedSubject);

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
            var dbChapter = _context.Chapters.FirstOrDefault(u => u.Id == chap.Id);

            // Get the semester Id
            var semesterId = _context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment)?.Id;

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
            var semesterId = (int?)TempData["Data_SemesterId"];

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