using System.Web.Mvc;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("/home")]
        public ActionResult Index()
        {
            Session["Username"] = null;
            return View();
        }   
    }
}