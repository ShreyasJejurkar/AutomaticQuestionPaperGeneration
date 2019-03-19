using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

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