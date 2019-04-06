using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class SemesterController : BaseController
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Semester> _data;
        
        public SemesterController()
        {
            _data = _context.Semesters;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_data.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Semester newSem)
        {
            // Add new semester to database and commit the operation.
            _context.Semesters.Add(newSem);
            _context.SaveChanges();

            return RedirectToAction("Index", _data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get the semester details from database
            var semester = _context.Semesters.FirstOrDefault(u => u.Id == id);

            // pass it to view
            return View("Edit", semester);
        }

        /// <summary>
        ///     Does a post request to DB making changes to Semester information
        /// </summary>
        /// <param name="editSemester">Object containing information about Semester</param>
        /// <returns>Returns a corresponding view</returns>
        [HttpPost]
        public ActionResult Edit(Semester editSemester)
        {
            // Get the details of old semester from database
            var semesterDb = _context.Semesters.FirstOrDefault(u => u.Id == editSemester.Id);

            // Save the new changes
            if (semesterDb != null)
                semesterDb.SemesterName = editSemester.SemesterName;

            // Commit it to database
            _context.SaveChanges();

            //Set the success message
            TempData["SemesterDeleteSuccessMessage"] = "Semester edited successfully";

            return RedirectToActionPermanent("Index", _data);
        }

        /// <summary>
        ///     Performs delete operation on Semester
        /// </summary>
        /// <param name="id">Takes Semester ID</param>
        /// <returns>Return corresponding view</returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                // Get the details of old semester
                var semesterDb = _context.Semesters.SingleOrDefault(u => u.Id == id);

                if (semesterDb != null)
                {
                    // Remove it from database and commit it
                    _context.Semesters.Remove(semesterDb);
                    _context.SaveChanges();

                    // Set the success message
                    ViewBag.SemesterDeleteSuccessMessage = "Semester deleted successfully";
                    return View("Index", _data);
                }

                ViewBag.SemesterDeleteFailtureMessage = "Something went wrong. Semester cannot be deleted";
                return View("Index", _data);
            }

            return View("Index", _data);
        }
    }
}