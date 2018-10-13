using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class AdminHomePageController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

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

                // Get the admin details from database 
                var admin = _context.Admins.FirstOrDefault(u => u.Username == (string) adminName);

                return View(admin);
            }
        }
    }
}