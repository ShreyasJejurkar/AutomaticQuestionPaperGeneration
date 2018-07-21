using System.Web.Mvc;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class AdminHomePageController : Controller
    {
        // GET: Admin/AdminHomePage
        public ActionResult Index()
        {
            return View("AdminHomePage");
        }
    }
}