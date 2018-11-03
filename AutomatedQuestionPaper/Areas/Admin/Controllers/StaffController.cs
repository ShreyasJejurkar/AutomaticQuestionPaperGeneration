using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheck]
    public class StaffController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        private Models.Staff _dbTeacher = new Models.Staff();

        [HttpGet]
        public ActionResult Index()
        {
            var data = _context.Staffs;

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult TeacherEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherEdit(Models.Staff teacher)
        {
            return View();
        }

        /// <summary>
        ///     Will do a POST request to DB for staff information
        /// </summary>
        /// <param name="id">ID of staff</param>
        /// <returns>Returns Corresponding view</returns>
        [HttpPost]
        public ActionResult GetStaffDetails(int id)
        {
            // Get the details of specified ID
            _dbTeacher = _context.Staffs.FirstOrDefault(u => u.Id == id);

            if (_dbTeacher == null)
            {
                // Set the message for View
                ViewBag.StaffNotFoundErrorMessage = "Staff details not found. Please ensure you entered correct ID";

                return View("TeacherEdit", null);
            }

            // Else pass the staff information to View
            return View("TeacherEdit", _dbTeacher);
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
        public ActionResult DeleteTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Staff/DeleteTeacher")]
        public ActionResult DeleteTeacher(int teacherId)
        {
            // Find the corresponding staff details as per ID
            var teacherDb = _context.Staffs.SingleOrDefault(u => u.Id == teacherId);

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
            return View();
        }
    }
}