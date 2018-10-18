using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheck]
    public class SemesterController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Semester> _data;

        public SemesterController() : this(1) { }

        public SemesterController(int sdata)
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
            //Add new semester to database and commit the operation.
            _context.Semesters.Add(newSem);
            _context.SaveChanges();

            return RedirectToAction("Index", _data);
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //Get the semester details from database
            var s = _context.Semesters.FirstOrDefault(u => u.Id == id);

            //pass it to view
            return View("Edit", s);
        }

        [HttpPost]
        public ActionResult Edit(Semester editSem)
        {
            //Get the details of old semester from database
            var semesterDb = _context.Semesters.FirstOrDefault(u => u.Id == editSem.Id);

            //Save the new changes
            if (semesterDb != null)
                semesterDb.SemesterName = editSem.SemesterName;

            //Commit it to database
            _context.SaveChanges();

            //Set the success message
            TempData["SemesterDeleteSuccessMessage"] = "Semester edited successfully";

            return RedirectToActionPermanent("Index", _data);
        }
        
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                //Get the details of old semester
                var semesterDb = _context.Semesters.SingleOrDefault(u => u.Id == id);

                if (semesterDb != null)
                {
                    //Remove it from database and commit it
                    _context.Semesters.Remove(semesterDb);
                    _context.SaveChanges();

                    //Set the success message
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