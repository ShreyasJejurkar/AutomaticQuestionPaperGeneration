using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (TempData["SessionErrorMessage"] != null)
            {
                Alert("Session error", (string)TempData["SessionErrorMessage"], Enums.NotificationType.error);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin user)
        {
            var auth = Authentication.Authenticate(user);

            if (auth.status == 0)
            {
                Alert("Error","Incorrect credentials", Enums.NotificationType.error);
                return View();
            }

            if (auth.status == 1 && auth.authenticatedUserName != null)
            {
                //Saving data to session for login functionality
                Session["Staff_Name"] = auth.authenticatedUserName;


                Alert("Welcome",$"Hello {Session["Staff_Name"]}", Enums.NotificationType.success);
                
                return RedirectToAction("Index", "StaffHomePage", new
                {
                    area = "Staff"
                });
            }

            if (auth.status == 2 && auth.authenticatedUserName != null)
            {
                //Saving data to session for login functionality
                Session["Username"] = auth.authenticatedUserName;

                Alert("Welcome", $"Hello {Session["Username"]}", Enums.NotificationType.success);

                return RedirectToAction("Index", "AdminHomePage", new
                {
                    area = "Admin"
                });
            }

            return View();
        }
    }
}