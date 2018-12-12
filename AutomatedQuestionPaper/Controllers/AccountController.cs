using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin user)
        {
            var auth = Authentication.Authenticate(user);

            if (auth.status == 0)
            {
                ViewBag.LoginErrorMessage = "Incorrect Credentials";
                return View();
            }

            if (auth.status == 1 && auth.authenticatedUserName != null)
            {
                //Saving data to session for login functionality
                Session["Staff_Name"] = auth.authenticatedUserName;

                return RedirectToAction("Index", "StaffHomePage", new
                {
                    area = "Staff"
                });
            }

            if (auth.status == 2 && auth.authenticatedUserName != null)
            {
                //Saving data to session for login functionality
                Session["Username"] = auth.authenticatedUserName;

                return RedirectToAction("Index", "AdminHomePage", new
                {
                    area = "Admin"
                });
            }

            return View();
        }
    }
}