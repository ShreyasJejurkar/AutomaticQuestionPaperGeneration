using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class ManualTypeController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Staff/ManualType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadQuestion(string selectedSemester, string selectedDepartment, string selectedSubject, string unitNo, string chapterName, string examType)
        {
            var semesterId = _context.Semesters.FirstOrDefault(x => x.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartment)?.Id;

            var subjectId = _context.Courses.FirstOrDefault(x => x.CourseName == selectedSubject)?.Courseid;

            var unitInt = Convert.ToInt32(unitNo);

            var type = (int)Enum.Parse(typeof(ExamType), examType);

            var chapterId = _context.Chapters.FirstOrDefault(x => x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId && x.UnitNo == unitInt && x.ChapterName == chapterName)?.Id;

            new Thread(() =>
            {
                TempData["Semester-id"] = semesterId;
                TempData["Department-id"] = departmentId;
                TempData["Subject-id"] = subjectId;
                TempData["Chapter-id"] = chapterId;
                TempData["Unit-id"] = unitInt;
                TempData["Type-id"] = type;
            }).Start();

            new Thread(() =>
            {
                TempData["Semester-Name"] = selectedSemester;
                TempData["Department-Name"] = selectedDepartment;
                TempData["Subject-Name"] = selectedSubject;
                TempData["Chapter-Name"] = chapterName;
                TempData["Unit-Name"] = unitNo;
                TempData["Type-Name"] = examType;
            }).Start();

            
            var questions = _context.Questions.Where(x => x.SemesterId == semesterId.ToString() && x.DepartmentId == departmentId.ToString() && x.CourseId == subjectId && x.UnitId == unitInt && x.ChapterId == chapterId && x.QuestionType == type).Select(x => x.QuestionText).ToList();

            return PartialView("QuestionList", questions);
        }

        public ActionResult AddQuestion(string selectedSemester, string selectedDepartment, string selectedSubject, string unitNo, string chapterName, string question, string examType)
        {
            // Get the semester Id
            var semesterId = _context.Semesters.FirstOrDefault(x => x.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartment)?.Id;

            var subjectId = _context.Courses.FirstOrDefault(x => x.CourseName == selectedSubject)?.Courseid;

            var unitInt = Convert.ToInt32(unitNo);

            var chapterId = _context.Chapters.FirstOrDefault(x => x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId && x.UnitNo == unitInt && x.ChapterName == chapterName)?.Id;

            var type = (int) Enum.Parse(typeof(ExamType), examType); 

            _context.Questions.Add(new Question
            {
                ChapterId = chapterId,
                CourseId = subjectId,
                DepartmentId = departmentId.ToString(),
                DifficultyLevel = 1,
                QuestionText = question,
                QuestionType = type,
                SemesterId = semesterId.ToString(),
                UnitId = unitInt
            });

            _context.SaveChangesAsync();

            TempData["QuestionAdded"] = "Question added successfully";

            return Json("Question added successfully", JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetChapterList(string semester, string department, string subject, string unit)
        {
            var semesterId = DatabaseData.GetSemesterInfo(semester)?.Id;
            
            var departmentId = DatabaseData.GetDepartmentInfo(department)?.Id;

            var subjectId = DatabaseData.GetCourseInfo(subject)?.Courseid;

            var unitInt = Convert.ToInt32(unit);

            var chapters = _context.Chapters.Where(x => x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId && x.UnitNo == unitInt).Select(x => x.ChapterName).ToList();

            return Json(chapters, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditQuestionDetails(string question)
        {
            var semesterId = Convert.ToString(TempData["Semester-id"]);
            var departmentId = Convert.ToString(TempData["Department-id"]);

            var subjectId = (int?) TempData["Subject-id"];
            var chapterId = (int?)TempData["Chapter-id"];
            var unitInt = (int?) TempData["Unit-id"];
            
            var dbQuestion = _context.Questions.FirstOrDefault(x =>
                x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                x.ChapterId == chapterId && x.QuestionText == question && x.UnitId == unitInt);

            // TODO lot of work has to be done in view. start with fetching allocated subject list


            return View(dbQuestion);
        }

        [HttpPost]
        public ActionResult EditQuestionDetails(string selectedSemester, string selectedDepartment, string selectedSubject, string selectedUnit, string chapterName, string question, int? QuestionHiddenId)
        {
            var num = Convert.ToInt32( Request.Form["Id"]);

            var dbQuestion = _context.Questions.FirstOrDefault(x=>x.Id == num);

            // Get the semester Id
            var semesterId = _context.Semesters.FirstOrDefault(x => x.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartment)?.Id;

            var subjectId = _context.Courses.FirstOrDefault(x => x.CourseName == selectedSubject)?.Courseid;

            var unitNo = Convert.ToInt32(selectedUnit);

            var chapters = _context.Chapters.FirstOrDefault(x => x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId && x.UnitNo == unitNo && x.ChapterName == chapterName)?.Id;

            dbQuestion.ChapterId = chapters;
            dbQuestion.CourseId = subjectId;
            dbQuestion.DepartmentId = Convert.ToString(departmentId);
            dbQuestion.DifficultyLevel = 1;
            dbQuestion.QuestionText = question;
            dbQuestion.QuestionType = 1;
            dbQuestion.SemesterId = Convert.ToString(semesterId);
            dbQuestion.UnitId = unitNo;

            TempData["QuestionEditedSuccessMessage"] = "Question edited successfully";

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult DeleteQuestion(string Id)
        {
            var questionId = Convert.ToInt32(Id);
            var question = _context.Questions.FirstOrDefault(x=>x.Id == questionId);

            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }

            TempData["QuestionDeletedSuccessMessage"] = "Question deleted successfully";

            return RedirectToAction("Index");
        }
    }
}