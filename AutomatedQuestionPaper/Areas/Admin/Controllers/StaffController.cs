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

        [HttpPost]
        public ActionResult GetStaffDetails(int id = 0)
        {
            _dbTeacher = _context.Staffs.FirstOrDefault(u => u.Id == id);

            if (_dbTeacher == null)
            {
                // ViewBag.StaffID = _dbTeacher.Id;
                ViewBag.StaffNotFoundErrorMessage = "Staff details not found. Please ensure you entered correct ID";
                return View("TeacherEdit", null);
            }

            return View("TeacherEdit", _dbTeacher);
        }

        [HttpPost]
        public ActionResult StaffEditSaveChanges(Models.Staff editedStaffDetails)
        {
            //Fetch the old data of staff from database
            var oldData = _context.Staffs.FirstOrDefault(u=> u.Id == editedStaffDetails.Id);

            //Setting up new data
            if (oldData != null)
            {
                oldData.Address = editedStaffDetails.Address;
                oldData.Email = editedStaffDetails.Email;
                oldData.Name = editedStaffDetails.Name;
                oldData.Phone = editedStaffDetails.Phone;
                oldData.Surname = editedStaffDetails.Surname;

                //Commit new changes to database
                _context.SaveChanges();

                //Set the success message
                TempData["StaffDetailsEditedSuccessfully"] = "Staff details edited successfully";
                return RedirectToAction("Index", "Staff");

            }
            else
            {
                TempData["StaffDetailsEditFailed"] = "Cannot edit staff details.";
                return RedirectToAction("Index", "Staff");
            }
        }

        [HttpGet]
        public ActionResult TeacherAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherAdd(Models.Staff data)
        {
            _context.Staffs.Add(data);
            _context.SaveChanges();

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
            var teacherDb = _context.Staffs.SingleOrDefault(u => u.Id == teacherId);

            if (teacherDb != null)
            {
                _context.Staffs.Remove(teacherDb);
                _context.SaveChanges();
                TempData["TeacherDeletedSuccessMessage"] = "Teacher deleted successfully";
                return RedirectToAction("Index", "Staff");
            }

            ViewBag.TeacherNotFoundErrorMessage = "Teacher does not exists. Please check ID";
            return View();
        }
    }
}