using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Controllers
{
    [SessionCheckStaff]
    public class StaffHomePageController : BaseController
    {
        /// <summary>
        ///     EF context for database access
        /// </summary>
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            var staffName = Session["Staff_Name"];

            // Get the staff details from database
            var staff = _context.Staffs.FirstOrDefault(u => u.Name == (string) staffName);

            return View(staff);
        }

        [HttpPost]
        public ActionResult SetPassword(string passwordField)
        {
            var staffName = Session["Staff_Name"];
            
            var staff = _context.Staffs.FirstOrDefault(u => u.Name == (string)staffName);

            if (staff != null)
            {
                staff.Password = passwordField;
                _context.SaveChanges();
                Alert("Success","Password set successfully",Enums.NotificationType.success);
                return RedirectToAction("Index");
            }
            else
            {
                Alert("Failed", "Something went wrong", Enums.NotificationType.error);
                return RedirectToAction("Index", "Home");

            }
        }
    }
}