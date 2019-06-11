using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class StaffHomePageController : AlertController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            var staff = (string) Session["Staff_Name"];
            return View(_context.Staffs.FirstOrDefault(m => m.Name == staff));
        }
    }
}