using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Areas.Staff.Models;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class QuestionPaperRepositoryController : BaseController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        // GET: Staff/QuestionPaperRepository
        public ActionResult Index()
        {
            var loggedInStaff = (string) Session["Staff_Name"];

            var staffId = _context.Staffs.FirstOrDefault(u => u.Name == loggedInStaff)?.Id;

            var data = _context.ExamPapers.Where(x => x.StaffId == staffId).Select(x => x).ToList();

            if (data.Count == 0)
            {
                TempData["NoQuestionPaper"] = "There are no previous question paper available";
            }

            return View(data);
        }

        public ActionResult QuestionPaperView(int id)
        {
            var data = PdfHandler.DisplayPdf(id);
            return data;
        }

        public ActionResult QuestionPaperDelete(int id)
        {
            var examPaper = _context.ExamPapers.FirstOrDefault(i => i.Id == id);
            _context.ExamPapers.Remove(examPaper);

            _context.SaveChanges();

            TempData["QuestionPaperDeleted"] = "Question paper deleted";

            return RedirectToAction("Index");
        }

        public ActionResult QuestionPaperDownloadWord(int id)
        {
            var data = WordHandler.DownloadWordFile(id);
            return data;
        }
    }
}