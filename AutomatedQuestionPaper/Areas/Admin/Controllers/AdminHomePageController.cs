using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class AdminHomePageController : AlertController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            var adminName = Session["Username"];
            var admin = _context.Admins.FirstOrDefault(u => u.Username == (string) adminName);
            return View(admin);
        }
    }
}