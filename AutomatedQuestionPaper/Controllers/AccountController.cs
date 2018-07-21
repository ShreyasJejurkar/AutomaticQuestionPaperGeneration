using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModelContainer _context = new ModelContainer();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin user)
        {
            //Check if user is admin or not!
            var dbuser = _context.Admins.FirstOrDefault(u => u.username == user.username && u.password == user.password);

            if (dbuser == null)
            {

                //Check for staff member
                var staffUser = _context.Teachers.FirstOrDefault(T => T.name == user.username && T.password == user.password);
                if (staffUser != null)
                {
                    return RedirectToAction("Index", "StaffHomePage", new
                    {
                        area = "Staff"
                    });
                }
            }

            if (dbuser != null)
            {
                return RedirectToAction("Index", "AdminHomePage", new
                {
                    area = "Admin"
                });
            }

            ViewBag.LoginErrorMessage = "Incorrect Credentials";
            return View();
        }
    }
}