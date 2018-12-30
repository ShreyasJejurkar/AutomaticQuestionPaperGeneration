using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class FileUploadController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Staff/FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuestionFileUpload(string selectedFileType, HttpPostedFileBase fileControl,
            string selectedSemester, string selectedDepartment, string selectedSubject, string selectedUnit,
            string ExamType, string chapterName)
        {
            var extension = Path.GetExtension(fileControl.FileName);

            // Get the Id of selected subject
            var subjectId = _context.Courses.FirstOrDefault(u => u.CourseName == selectedSubject)?.Courseid;

            // Get the Id of selected department 
            var departmentId = DatabaseData.GetDepartmentInfo(selectedDepartment)?.Id;

            // Get the Id of selected semester 
            var semesterId = DatabaseData.GetSemesterInfo(selectedSemester)?.Id;

            var unit = Convert.ToInt32(selectedUnit);

            var type = (int) Enum.Parse(typeof(ExamType), ExamType);

            var chapterId = _context.Chapters.FirstOrDefault(x =>
                x.SemesterId == semesterId && x.DepartmentId == departmentId && x.CourseId == subjectId &&
                x.UnitNo == unit && x.ChapterName == chapterName)?.Id;

            // Word file
            if (extension == ".docx" || extension == ".doc")
            {
                ProcessFile.WordFile(fileControl);
            }

            // CSV file
            else if (extension == ".csv" || extension == ".CSV")
            {
                var data = ProcessFile.CsvFile(fileControl);
                if (data.questions == null)
                {
                    TempData["UploadError"] = data.error;
                    return View("Index");
                }

                foreach (var q in data.questions)
                    _context.Questions.Add(new Question
                    {
                        ChapterId = chapterId,
                        CourseId = subjectId,
                        DepartmentId = Convert.ToString(departmentId),
                        DifficultyLevel = q.Level,
                        QuestionText = q.Question,
                        QuestionType = type,
                        SemesterId = Convert.ToString(semesterId),
                        UnitId = unit
                    });
                _context.SaveChangesAsync();

                TempData["QuestionAddedSuccessfully"] = "Question set added successfully";

                return RedirectToAction("Index", "Question");
            }

            // Excel file
            else if (extension == ".xls")
            {
                ProcessFile.ExcelFile(fileControl);
            }

            return null;
        }
    }
}