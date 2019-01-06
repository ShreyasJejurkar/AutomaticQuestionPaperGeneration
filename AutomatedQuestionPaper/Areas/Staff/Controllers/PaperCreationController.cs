using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class PaperCreationController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public ActionResult Index()
        {
            var list = _context.ExamPapers.ToList();

            return View(list);
        }
    }
}