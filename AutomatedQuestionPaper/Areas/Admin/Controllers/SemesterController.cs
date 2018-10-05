using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class SemesterController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Semester> _data;

        public SemesterController() : this(1) { }

        public SemesterController(int sdata)
        {
            _data = _context.Semesters;
        }

        public ActionResult Index()
        {
            return View(_data.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var s = _context.Semesters.FirstOrDefault(u => u.Id == id);
            return View("Edit", s);
        }

        [HttpPost]
        public ActionResult Edit(Semester editSem)
        {
            var semesterDb = _context.Semesters.FirstOrDefault(u => u.Id == editSem.Id);
            if (semesterDb != null)
                semesterDb.SemesterName = editSem.SemesterName;

            _context.SaveChanges();
            TempData["SemesterDeleteSuccessMessage"] = "Semester edited successfully";
            return RedirectToActionPermanent("Index", _data);
        }

        /*
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }
        */

        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                var semesterDb = _context.Semesters.SingleOrDefault(u => u.Id == id);

                if (semesterDb != null)
                {
                    _context.Semesters.Remove(semesterDb);
                    _context.SaveChanges();
                    ViewBag.SemesterDeleteSuccessMessage = "Semester deleted successfully";
                    return View("Index", _data);
                }

                ViewBag.SemesterDeleteFailtureMessage = "Something went wrong. Semester cannot be deleted";
                return View("Index", _data);
            }

            return View("Index", _data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Semester newSem)
        {
            _context.Semesters.Add(newSem);
            _context.SaveChanges();
            return RedirectToAction("Index", _data);
        }
    }
}