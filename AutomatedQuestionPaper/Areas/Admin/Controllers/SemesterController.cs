using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class SemesterController : Controller
    {
        private readonly SampleContext _context = new SampleContext();
        private readonly DbSet<semster> _data;

        public SemesterController() : this(1) { }

        public SemesterController(int sdata)
        {
            _data = _context.semsters;
        }

        public ActionResult Index()
        {
            return View(_data.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var s = _context.semsters.FirstOrDefault(u => u.id == id);
            return View("Edit", s);
        }

        [HttpPost]
        public ActionResult Edit(semster editSem)
        {
            var semesterDb = _context.semsters.FirstOrDefault(u => u.id == editSem.id);
            if (semesterDb != null)
                semesterDb.semester = editSem.semester;

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
                var semesterDb = _context.semsters.SingleOrDefault(u => u.id == id);

                if (semesterDb != null)
                {
                    _context.semsters.Remove(semesterDb);
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
        public ActionResult Create(semster newSem)
        {
            _context.semsters.Add(newSem);
            _context.SaveChanges();
            return RedirectToAction("Index", _data);
        }
    }
}