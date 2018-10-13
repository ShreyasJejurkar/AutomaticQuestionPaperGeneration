using System;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
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
        public ActionResult StaffEditSaveChanges()
        {
            var keys = Request.Form.AllKeys;

            var id = Convert.ToInt32(Request.Form.Get(keys[1]));

            var oldStaffData = _context.Staffs.FirstOrDefault(u => u.Id == id);

            if (oldStaffData != null)
            {
                oldStaffData.Name = Request.Form.Get(keys[2]);
                oldStaffData.Surname = Request.Form.Get(keys[3]);
                oldStaffData.Address = Request.Form.Get(keys[4]);
                oldStaffData.Email = Request.Form.Get(keys[6]);
                oldStaffData.Password = Request.Form.Get(keys[7]);
                oldStaffData.Phone = Request.Form.Get(keys[5]);
                _context.SaveChanges();
                TempData["StaffEditSuccessMessage"] = "Staff details has been saved successfully";
                return View("Index");
            }

            return null;
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