using System.Web.Mvc;

namespace AutomatedQuestionPaper.Controllers
{
    public class HomeController : AlertController
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