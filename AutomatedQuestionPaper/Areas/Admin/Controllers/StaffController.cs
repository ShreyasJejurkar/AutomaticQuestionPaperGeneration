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
        public ActionResult TeacherAdd(Teacher data)
        {
            data.TeacherCourse = new TeacherCourse
            {
                SemisterId = 2
            };

            data.secret_question = "";
            data.answer = "";

            _context.Teachers.Add(data);
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
            var teacherDb = _context.Teachers.SingleOrDefault(u => u.Id == TeacherID);

            if (teacherDb != null)
            {
                _context.Teachers.Remove(teacherDb);
                _context.SaveChanges();
                TempData["TeacherDeletedSuccessMessage"] = "Teacher deleted successfully";
                return RedirectToAction("Index", "Staff");
            }

            ViewBag.TeacherNotFoundErrorMessage = "Teacher does not exists. Please check ID";
            return View();
        }
    }
}