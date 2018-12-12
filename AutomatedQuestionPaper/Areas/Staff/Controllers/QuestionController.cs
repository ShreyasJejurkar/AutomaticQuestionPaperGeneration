using System.Web.Mvc;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class QuestionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}