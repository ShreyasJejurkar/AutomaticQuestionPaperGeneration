using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class AdminHomePageController : BaseController
    {
        /// <summary>
        /// Object responsible for database connection and querying
        /// </summary>
        private readonly DatabaseContext _context = new DatabaseContext();

        /// <summary>
        /// Action displays Admin Home page. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var adminName = Session["Username"];

            // Get the admin details from database 
            var admin = _context.Admins.FirstOrDefault(u => u.Username == (string) adminName);

            return View(admin);
        }
    }
}