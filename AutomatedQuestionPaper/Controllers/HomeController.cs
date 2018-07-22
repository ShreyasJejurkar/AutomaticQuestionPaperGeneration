using System.Web.Mvc;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }   
    }
}