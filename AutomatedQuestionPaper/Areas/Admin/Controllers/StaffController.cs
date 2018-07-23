using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        private readonly ModelContainer _context = new ModelContainer();

        // GET: Admin/Staff
        public ActionResult Index()
        {
            var data = _context.Teachers;

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult TeacherEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherEdit(Teacher teacher)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStaffDetails(int id)
        {
            var dbTeacher = _context.Teachers.FirstOrDefault(u => u.Id == id);

            if (dbTeacher == null)
            {
                ViewBag.StaffNotFoundErrorMessage = "Staff details not found. Please ensure you entered correct ID";
                return View("TeacherEdit",null);
            }

            return View("TeacherEdit", dbTeacher);
        }
    }
}