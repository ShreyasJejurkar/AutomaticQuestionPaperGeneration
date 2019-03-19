using System;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;
using System.Collections.Generic;

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

        public ActionResult AddQuestion(string selectedSemester, string selectedDepartment, string selectedSubject,
            string unitNo, string chapterName, string question, string examType, string level)
        {
            // Get the semester Id
            var semesterId = DatabaseData.GetSemesterInfo(selectedSemester).Id;

            // Get the department Id
            var departmentId = DatabaseData.GetDepartmentInfo(selectedDepartment).Id;

            var subjectId = DatabaseData.GetCourseInfo(selectedSubject).Courseid;

            var unitInt = Convert.ToInt32(unitNo);

            var chapterId = _context.Chapters.FirstOrDefault(x =>
                x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                x.UnitNo == unitInt && x.ChapterName == chapterName)?.Id;

            var type = (int)Enum.Parse(typeof(ExamType), examType);


            var questionsList = _context.Questions.Where(x => x.SemesterId == semesterId.ToString()
                                                              && x.DepartmentId == departmentId.ToString()
                                                              && x.CourseId == subjectId
                                                              && x.UnitId == unitInt
                                                              && x.ChapterId == chapterId
                                                              && x.QuestionType == type)
                                                              .Select(x => x.QuestionText)
                .ToList();


            var client = new HttpClient();
            var url = "http://127.0.0.1:5000/semantic";


            if (questionsList.Count != 0)
            {
                //foreach (var que in questionsList)
                //{
                //    var data = new
                //    {
                //        first_text = question,
                //        second_text = que
                //    };

                //    var cli = new WebClient();
                //    cli.Headers[HttpRequestHeader.ContentType] = "application/json";
                //    var response = cli.UploadString(url, JsonConvert.SerializeObject(data));
                //    var output = JsonConvert.DeserializeObject<ServerOutputData>(response);

                //}

                _context.Questions.Add(new Question
                {
                    ChapterId = chapterId,
                    CourseId = subjectId,
                    DepartmentId = departmentId.ToString(),
                    DifficultyLevel = Convert.ToInt32(level),
                    QuestionText = question,
                    QuestionType = type,
                    SemesterId = semesterId.ToString(),
                    UnitId = unitInt
                });
            }
            else
            {
                _context.Questions.Add(new Question
                {
                    ChapterId = chapterId,
                    CourseId = subjectId,
                    DepartmentId = departmentId.ToString(),
                    DifficultyLevel = Convert.ToInt32(level),
                    QuestionText = question,
                    QuestionType = type,
                    SemesterId = semesterId.ToString(),
                    UnitId = unitInt
                });
            }


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

            var chapters = _context.Chapters
                .Where(x => x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                            x.UnitNo == unitInt).Select(x => x.ChapterName).ToList();

            return Json(chapters, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditQuestionDetails(string question, string level, string semester, string department,
            string chapter, string type, string unit)
        {
            var levelInt = Convert.ToInt32(level);
            var chap = Convert.ToInt32(chapter);
            var quesType = Convert.ToInt32(type);
            var unitNo = Convert.ToInt32(unit);
            var ques = _context.Questions.FirstOrDefault(t =>
                t.QuestionText == question &&
                t.DifficultyLevel == levelInt &&
                t.SemesterId == semester &&
                t.DepartmentId == department &&
                t.ChapterId == chap &&
                t.QuestionType == quesType &&
                t.UnitId == unitNo
            );
            var sem = Convert.ToInt32(ques.SemesterId);
            var dep = Convert.ToInt32(ques.DepartmentId);

            TempData["Semester-Name"] = _context.Semesters.FirstOrDefault(x => x.Id == sem)?.SemesterName;
            TempData["Department-Name"] = _context.Departments.FirstOrDefault(x => x.Id == dep)?.DepartmentName;
            TempData["Subject-Name"] = _context.Courses.FirstOrDefault(x => x.Courseid == ques.CourseId)?.CourseName;
            TempData["Chapter-Name"] = _context.Chapters.FirstOrDefault(x => x.Id == ques.ChapterId)?.ChapterName;
            TempData["Level"] = level;

            if (ques.QuestionType == 0)
                TempData["Type-Name"] = "InSem";
            else if (ques.QuestionType == 1) TempData["Type-Name"] = "EndSem";

            // TODO lot of work has to be done in view. start with fetching allocated subject list


            return View(ques);
        }

        [HttpPost]
        public ActionResult EditQuestionDetails(string selectedSemester, string selectedDepartment,
            string selectedSubject, string selectedUnit, string chapterName, string question, int? QuestionHiddenId,
            string difficultyLevel, string ExamType)
        {
            var num = Convert.ToInt32(Request.Form["Id"]);

            var dbQuestion = _context.Questions.FirstOrDefault(x => x.Id == num);

            // Get the semester Id
            var semesterId = _context.Semesters.FirstOrDefault(x => x.SemesterName == selectedSemester)?.Id;

            // Get the department Id
            var departmentId = _context.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartment)?.Id;

            var subjectId = _context.Courses.FirstOrDefault(x => x.CourseName == selectedSubject)?.Courseid;

            var unitNo = Convert.ToInt32(selectedUnit);

            var type = (int)Enum.Parse(typeof(ExamType), ExamType);

            var chapters = _context.Chapters.FirstOrDefault(x =>
                x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                x.UnitNo == unitNo && x.ChapterName == chapterName)?.Id;

            dbQuestion.ChapterId = chapters;
            dbQuestion.CourseId = subjectId;
            dbQuestion.DepartmentId = Convert.ToString(departmentId);
            dbQuestion.DifficultyLevel = Convert.ToInt32(difficultyLevel);
            dbQuestion.QuestionText = question;
            dbQuestion.QuestionType = type;
            dbQuestion.SemesterId = Convert.ToString(semesterId);
            dbQuestion.UnitId = unitNo;

            TempData["QuestionEditedSuccessMessage"] = "Question edited successfully";

            _context.SaveChanges();

            return RedirectToAction("QuestionRepository");
        }

        [HttpGet]
        public ActionResult DeleteQuestion(string Id)
        {
            var questionId = Convert.ToInt32(Id);
            var question = _context.Questions.FirstOrDefault(x => x.Id == questionId);

            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }

            TempData["QuestionDeletedSuccessMessage"] = "Question deleted successfully";

            return RedirectToAction("QuestionRepository");
        }

        [HttpGet]
        public ActionResult QuestionRepository()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuestionRepository(string selectedSemester, string selectedDepartment,
            string selectedSubject, string unitNo, string chapterName, string examType, string searchText)
        {
            if (searchText == null)
            {
                var semesterId = _context.Semesters.FirstOrDefault(x => x.SemesterName == selectedSemester)?.Id;

                // Get the department Id
                var departmentId = _context.Departments.FirstOrDefault(x => x.DepartmentName == selectedDepartment)?.Id;

                var subjectId = _context.Courses.FirstOrDefault(x => x.CourseName == selectedSubject)?.Courseid;

                var unitInt = Convert.ToInt32(unitNo);

                var type = (int)Enum.Parse(typeof(ExamType), examType);

                var chapterId = _context.Chapters.FirstOrDefault(x =>
                    x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                    x.UnitNo == unitInt && x.ChapterName == chapterName)?.Id;

                var questions = _context.Questions
                    .Where(x => x.SemesterId == semesterId.ToString() && x.DepartmentId == departmentId.ToString() &&
                                x.CourseId == subjectId && x.UnitId == unitInt && x.ChapterId == chapterId &&
                                x.QuestionType == type).Select(x => new QuestionFormat
                                {
                                    Question = x.QuestionText,
                                    Level = x.DifficultyLevel,
                                    Semester = x.SemesterId,
                                    Department = x.DepartmentId,
                                    Chapter = x.ChapterId,
                                    QuestionType = x.QuestionType,
                                    UnitId = x.UnitId
                                }).ToList();

                TempData["QuestionList"] = questions;


                return PartialView("QuestionList", questions);
            }
            else
            {
                var questionsList = (List<QuestionFormat>)TempData["QuestionList"];

                var questionWithSearch = questionsList
                    .Where(x => x.Question.StartsWith(searchText)).Select(x => new QuestionFormat
                    {
                        Question = x.Question,
                        Level = x.Level,
                        Semester = x.Semester,
                        Department = x.Department,
                        Chapter = x.Chapter,
                        QuestionType = x.QuestionType,
                        UnitId = x.UnitId
                    }).ToList();

                return PartialView("QuestionList", questionWithSearch);
            }
        }
    }
}