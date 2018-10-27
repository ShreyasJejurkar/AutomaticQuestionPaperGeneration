using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin user)
        {
            //Check if user is admin or not!
            var dbUser = _context.Admins.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (dbUser == null)
            {
                //Check for staff member
                var staffUser = _context.Staffs.FirstOrDefault(T => T.Name == user.Username && T.Password == user.Password);

                if (staffUser != null)
                {
                    //Saving data to session for login functionality
                    Session["Username"] = staffUser.Name;

                    return RedirectToAction("Index", "StaffHomePage", new
                    {
                        area = "Staff"
                    });
                }
            }

            if (dbUser != null)
            {
                //Saving data to session for login functionality
                Session["Username"] = dbUser.Username;

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