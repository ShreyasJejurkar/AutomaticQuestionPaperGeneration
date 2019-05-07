using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    public class ParagraphQuestionController : BaseController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NextStep(string selectedSemester, string selectedDepartment, string selectedSubject, string examType, string chapterName, string unitNo)
        {
            TempData["Para_Semester"] = selectedSemester;
            TempData["Para_Department"] = selectedDepartment;
            TempData["Para_Subject"] = selectedSubject;
            TempData["Para_Chapter"] = chapterName;
            TempData["Para_Unit"] = unitNo;
            TempData["Para_ExamType"] = examType;

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParagraph()
        {
            return View();
        }

        public ActionResult GenerateQuestion(string paragraphContent)
        {
            var data = GetQuestions(paragraphContent);

            return PartialView("ParagraphGeneratedQuestionList", data);
        }

        public List<string> GetQuestions(string content)
        {
            const string directory = @"C:\Paragraph-question-generator";

            System.IO.File.WriteAllText(@"C:\Paragraph-question-generator\file.txt", content);
            var proc = new Process
            {
                StartInfo =
                {
                    WorkingDirectory = directory,
                    UseShellExecute = false,
                    FileName = @"C:\Paragraph-question-generator\q.bat",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            proc.WaitForExit();
            proc.Close();
            proc.Dispose();

            var generatedQuestionList = System.IO.File.ReadLines(@"C:\Paragraph-question-generator\output.txt").ToList();
            System.IO.File.WriteAllText(@"C:\Paragraph-question-generator\output.txt", string.Empty);
            return generatedQuestionList;
        }

        [HttpPost]
        public ActionResult AddQuestion(string[] selectedQuestions, int[] selectedLevel)
        {
            var semester = (string)TempData["Para_Semester"];
            var department = (string)TempData["Para_Department"];
            var subject = (string)TempData["Para_Subject"];
            var chapter = (string)TempData["Para_Chapter"];
            var unit = (string)TempData["Para_Unit"];
            var type = (string)TempData["Para_ExamType"];


            List<int> level = selectedLevel.Where(item => item != 0).ToList();

            for (var i = 0; i < selectedQuestions.Length; i++)
            {
                var newQuestion = new Question
                {
                    UnitId = Convert.ToInt32(unit),
                    SemesterId = Convert.ToString(DatabaseData.GetSemesterInfo(semester).Id),
                    ChapterId = _context.Chapters.FirstOrDefault(k => k.ChapterName == chapter)?.Id,
                    CourseId = DatabaseData.GetCourseInfo(subject).Courseid,
                    DepartmentId = Convert.ToString(DatabaseData.GetDepartmentInfo(department).Id),
                    DifficultyLevel = Convert.ToInt32(level[i]),
                    QuestionText = selectedQuestions[i],
                    QuestionType = (int)Enum.Parse(typeof(ExamType), type),
                    Answers = null
                };

                _context.Questions.Add(newQuestion);
            }

            _context.SaveChanges();

            Alert("Success", "Question added successfully", Enums.NotificationType.success);

            return View("Index");
        }
    }
}