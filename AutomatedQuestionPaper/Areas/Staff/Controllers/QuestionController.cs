using System.Web.Mvc;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Staff/Question
        public ActionResult Index()
        {
            return View();
        }
    }
}