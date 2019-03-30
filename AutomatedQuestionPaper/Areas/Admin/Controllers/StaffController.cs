using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;
using X.PagedList;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class StaffController : BaseController
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        [HttpGet]
        public ActionResult Index(string searchOption, string searchText, int? page)
        {
            if (searchText == null && searchOption == null)
            {
                return View(_context.Staffs.ToList().ToPagedList(page ?? 1, 10));
            }
            
            if (searchOption == "Name")
            {
                return View(_context.Staffs
                    .Where(x => x.Name.StartsWith(searchText)).ToList().ToPagedList(page ?? 1, 10));
            }

            if (searchOption == "Email")
            {
                return View(_context.Staffs
                    .Where(x => x.Email.StartsWith(searchText)).ToList().ToPagedList(page ?? 1, 10));

            }

            if (searchOption == "Phone Number")
            {
                return View(_context.Staffs
                    .Where(x => x.Phone.StartsWith(searchText)).ToList().ToPagedList(page ?? 1, 10));

            }

            return View();
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
            data.Password = "test_test";

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

        [HttpPost]
        public ActionResult DeleteMultiple(IEnumerable<int> selectedIds)
        {
            if (selectedIds == null)
            {
                Alert("","Select at least one staff record to delete",Enums.NotificationType.warning);
                return RedirectToAction("Index");
            }
            else
            {
                _context.Staffs.Where(x => selectedIds.Contains(x.Id))
                    .ToList().ForEach(p => _context.Staffs.Remove(p));
                _context.SaveChanges();
                Alert("Successful", "Selected staff records deleted", Enums.NotificationType.success);
                return RedirectToAction("Index");
            }
        }
    }
}