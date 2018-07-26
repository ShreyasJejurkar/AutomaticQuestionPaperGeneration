using System;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        private readonly ModelContainer _context = new ModelContainer();

        private Teacher _dbTeacher = new Teacher();

        [HttpGet]
        public ActionResult Index()
        {
            var data = _context.Teachers;

            return View(data.ToList());
        }

        [HttpGet]
        public ActionResult TeacherEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TeacherEdit(Teacher teacher)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStaffDetails(int id = 0)
        {
            _dbTeacher = _context.Teachers.FirstOrDefault(u => u.Id == id);
            ViewBag.StaffID = _dbTeacher.Id;

            if (_dbTeacher == null)
            {
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

            var oldStaffData = _context.Teachers.FirstOrDefault(u => u.Id == id);

            if (oldStaffData != null)
            {
                oldStaffData.name = Request.Form.Get(keys[2]);
                oldStaffData.surname = Request.Form.Get(keys[3]);
                oldStaffData.address = Request.Form.Get(keys[4]);
                oldStaffData.email = Request.Form.Get(keys[6]);
                oldStaffData.password = Request.Form.Get(keys[7]);
                oldStaffData.phone = Request.Form.Get(keys[5]);
                _context.SaveChanges();
                TempData["StaffEditSucessMessage"] = "Staff details has been saved sucessfully";
                return View("Index");
            }

            return null;
        }
    }
}