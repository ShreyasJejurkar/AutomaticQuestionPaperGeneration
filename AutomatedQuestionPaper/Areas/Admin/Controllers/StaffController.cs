using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class StaffController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index()
        {
            var data = _context.Staffs;

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult TeacherEdit(int id)
        {
            // Find the corresponding staff details as per ID
            var teacherDb = _context.Staffs.SingleOrDefault(u => u.Id == id);

            return View("TeacherEdit", teacherDb);
        }

        [HttpPost]
        public ActionResult StaffEditSaveChanges(Models.Staff editedStaffDetails)
        {
            // Fetch the old data of staff from database
            var oldData = _context.Staffs.FirstOrDefault(u => u.Id == editedStaffDetails.Id);

            // Setting up new data
            if (oldData != null)
            {
                oldData.Address = editedStaffDetails.Address;
                oldData.Email = editedStaffDetails.Email;
                oldData.Name = editedStaffDetails.Name;
                oldData.Phone = editedStaffDetails.Phone;
                oldData.Surname = editedStaffDetails.Surname;

                // Commit new changes to database
                _context.SaveChanges();

                // Set the success message
                TempData["StaffDetailsEditedSuccessfully"] = "Staff details edited successfully";
                return RedirectToAction("Index", "Staff");
            }

            TempData["StaffDetailsEditFailed"] = "Cannot edit staff details.";
            return RedirectToAction("Index", "Staff");
        }

        [HttpGet]
        public ActionResult TeacherAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherAdd(Models.Staff data)
        {
            // Add it to context and commit it to DB
            _context.Staffs.Add(data);
            _context.SaveChanges();

            // Set the success message  
            TempData["StaffAddedMessage"] = "Staff added successfully";

            return RedirectToAction("Index", "Staff");
        }

        [HttpGet]
        public ActionResult DeleteTeacher(int id)
        {
            // Find the corresponding staff details as per ID
            var teacherDb = _context.Staffs.SingleOrDefault(u => u.Id == id);

            // Make sure its not null
            if (teacherDb != null)
            {
                // Remove it from context and commit operation to database
                _context.Staffs.Remove(teacherDb);
                _context.SaveChanges();

                // Set the success message
                TempData["TeacherDeletedSuccessMessage"] = "Teacher deleted successfully";

                return RedirectToAction("Index", "Staff");
            }

            // Set the fail message if teacher ID not found. 
            ViewBag.TeacherNotFoundErrorMessage = "Teacher does not exists. Please check ID";

            return View("Index");
        }
    }
}