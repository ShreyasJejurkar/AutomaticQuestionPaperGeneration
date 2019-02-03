using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class PaperCreationController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly Random _r = new Random();

        public ActionResult Index()
        {
            return View("Index", null);
        }

        [HttpPost]
        public ActionResult Check(string selectedSemester, string selectedDepartment, string selectedSubject, string ExamType)
        {
            var errorList = new ErrorMessagesList();

            TempData["QuestionPaperDepartment"] = selectedDepartment;
            TempData["QuestionPaperSubject"] = selectedSubject;

            var semesterId = DatabaseData.GetSemesterInfo(selectedSemester).Id.ToString();
            var departmentId = DatabaseData.GetDepartmentInfo(selectedDepartment).Id.ToString();
            var subjectId = DatabaseData.GetCourseInfo(selectedSubject).Courseid;



            // TODO Apply same kind of mapping on other position of antonymous class to strongly build class
            var inSemQuestions = _context.Questions.Where(x => x.SemesterId == semesterId
                                                               && x.DepartmentId == departmentId
                                                               && x.CourseId == subjectId
                                                               && x.UnitId == 1 || x.UnitId == 2 || x.UnitId == 3)
                .Select(x => new PaperCreationQuestionFormat()
                {
                    Question = x.QuestionText,
                    Level = x.DifficultyLevel,
                    Unit = x.UnitId
                }).ToList();

            TempData["FetchedQuestions"] = inSemQuestions;


            var unitNo1 = inSemQuestions.Count(x => x.Unit == 1);
            var unitNo2 = inSemQuestions.Count(x => x.Unit == 2);
            var unitNo3 = inSemQuestions.Count(x => x.Unit == 3);

            TempData["Unit1Count"] = unitNo1;
            TempData["Unit2Count"] = unitNo2;
            TempData["Unit3Count"] = unitNo3;


            if (unitNo1 <= 4)
            {
                errorList.Add(new ErrorMessages
                {
                    ErrorText = $"At least 5 questions from unit 1 should be available (Current count {unitNo1})"
                });
            }

            if (unitNo2 <= 4)
            {
                errorList.Add(new ErrorMessages
                {
                    ErrorText = $"At least 5 questions from unit 2 should be available (Current count {unitNo2})"
                });
            }

            if (unitNo3 <= 4)
            {
                errorList.Add(new ErrorMessages
                {
                    ErrorText = $"At least 5 questions from unit 3 should be available (Current count {unitNo3})"
                });
            }

            TempData["Errors"] = errorList;

            return View("Index", errorList);
        }

        public ActionResult CreateQuestionPaper()
        {
            var questions = (List<PaperCreationQuestionFormat>)TempData["FetchedQuestions"];

            List<PaperCreationQuestionFormat> unit1QuestionsList =
                questions.Where(x => x.Unit == 1).Select(x => new PaperCreationQuestionFormat
                {
                    Question = x.Question,
                    Unit = x.Unit,
                    Level = x.Level
                }).ToList();

            List<PaperCreationQuestionFormat> unit2QuestionsList =
                questions.Where(x => x.Unit == 2).Select(x => new PaperCreationQuestionFormat
                {
                    Question = x.Question,
                    Unit = x.Unit,
                    Level = x.Level
                }).ToList();

            List<PaperCreationQuestionFormat> unit3QuestionsList =
                questions.Where(x => x.Unit == 3).Select(x => new PaperCreationQuestionFormat
                {
                    Question = x.Question,
                    Unit = x.Unit,
                    Level = x.Level
                }).ToList();

            List<PaperCreationQuestionFormat> formedQuestionSetUnit1 = new List<PaperCreationQuestionFormat>();
            List<PaperCreationQuestionFormat> formedQuestionSetUnit2 = new List<PaperCreationQuestionFormat>();
            List<PaperCreationQuestionFormat> formedQuestionSetUnit3 = new List<PaperCreationQuestionFormat>();

            while (unit1QuestionsList.Count != 0 && formedQuestionSetUnit1.Count != 4)
            {
                var unit1Random = _r.Next(1, unit1QuestionsList.Count);

                formedQuestionSetUnit1.Add(unit1QuestionsList[unit1Random]);
                unit1QuestionsList.Remove(unit1QuestionsList[unit1Random]);
            }

            while (unit2QuestionsList.Count != 0 && formedQuestionSetUnit2.Count != 4)
            {
                var unit2Random = _r.Next(1, unit2QuestionsList.Count);

                formedQuestionSetUnit2.Add(unit2QuestionsList[unit2Random]);
                unit2QuestionsList.Remove(unit2QuestionsList[unit2Random]);
            }

            while (unit3QuestionsList.Count != 0 && formedQuestionSetUnit3.Count != 4)
            {
                var unit3Random = _r.Next(1, unit3QuestionsList.Count);

                formedQuestionSetUnit3.Add(unit3QuestionsList[unit3Random]);
                unit3QuestionsList.Remove(unit3QuestionsList[unit3Random]);
            }

            ViewData["Question1And2"] = formedQuestionSetUnit1;
            ViewData["Question3And4"] = formedQuestionSetUnit2;
            ViewData["Question5And6"] = formedQuestionSetUnit3;

            return View();
        }

        [HttpPost]
        public ActionResult ValidateQuestionPaper(List<string> question)
        {
            var insem = new InSemQuestionPaperGenerator
            {
                Department_Name = (string) TempData["QuestionPaperDepartment"].ToString().Replace("department",""),
                Question1_A = question[0],
                Question1_B = question[1],
                Question2_A = question[2],
                Question2_B = question[3],
                Question3_A = question[4],
                Question3_B = question[5],
                Question4_A = question[6],
                Question4_B = question[7],
                Question5_A = question[8],
                Question5_B = question[9],
                Question6_A = question[10],
                Question6_B = question[11],
                Subject_Name = (string) TempData["QuestionPaperSubject"]
            };

            insem.GenerateQuestionPaper(question[12]+ " " + $"{DateTime.Now.ToString().Replace('/', '-').Replace(':', '.')}");

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}