using System;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        private readonly SampleContext _context = new SampleContext();

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
            _dbTeacher = _context.Staffs.FirstOrDefault(u => u.id == id);


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

            var oldStaffData = _context.Staffs.FirstOrDefault(u => u.id == id);

            if (oldStaffData != null)
            {
                oldStaffData.name = Request.Form.Get(keys[2]);
                oldStaffData.surname = Request.Form.Get(keys[3]);
                oldStaffData.address = Request.Form.Get(keys[4]);
                oldStaffData.email = Request.Form.Get(keys[6]);
                oldStaffData.password = Request.Form.Get(keys[7]);
                oldStaffData.phone = Request.Form.Get(keys[5]);
                _context.SaveChanges();
                TempData["StaffEditSucessMessage"] = "Staff details has been saved successfully";
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
           // data.secret_question = "";
            //data.answer = "";

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
        public ActionResult DeleteTeacher(int TeacherID = 0)
        {
            var teacherDb = _context.Staffs.SingleOrDefault(u => u.id == TeacherID);

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