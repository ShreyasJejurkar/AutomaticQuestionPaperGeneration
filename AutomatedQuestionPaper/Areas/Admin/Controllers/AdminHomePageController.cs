using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class AdminHomePageController : Controller
    {
        private readonly SampleContext _context = new SampleContext();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] == null )
            {
                ViewBag.SessionErrorMessage = "Please log in to your account first.";
                return View();
            }
            else
            {
                var adminName = Session["Username"];
                var admin = _context.Admins.FirstOrDefault(u => u.username == (string) adminName);

                return View(admin);
            }
        }
    }
}