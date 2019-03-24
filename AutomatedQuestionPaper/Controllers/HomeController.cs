using System.Web.Mvc;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            Session["Username"] = Session["Staff_Name"] = null;
            return View();
        }
    }
}