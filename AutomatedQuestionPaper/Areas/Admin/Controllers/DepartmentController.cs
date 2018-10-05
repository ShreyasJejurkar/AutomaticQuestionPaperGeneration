using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Department> _data;

        public DepartmentController() : this(1) { }
        public DepartmentController(int sdata)
        {
            _data = _context.Departments;
        }
        
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
        public ActionResult Create(Department dept)
        {
            _context.Departments.Add(dept);
            _context.SaveChanges();
            TempData["DepartmentAddedSuccessMessage"] = "Department added successfully";
            return RedirectToAction("Index", _data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = _context.Departments.FirstOrDefault(u => u.Id == id);
            return View("Edit", department);
        }

        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            var semesterDb = _context.Departments.FirstOrDefault(u => u.Id== dep.Id);
            if (semesterDb != null)
                semesterDb.DepartmentName = dep.DepartmentName;

            _context.SaveChanges();
            TempData["DepartmentDeleteSuccessMessage"] = "Department edited successfully";
            return RedirectToActionPermanent("Index", _data);
        }

        //public ActionResult Details(int id)
        //{
        //    throw new System.NotImplementedException();
        //}

        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                var semesterDb = _context.Departments.SingleOrDefault(u => u.Id== id);

                if (semesterDb != null)
                {
                    _context.Departments.Remove(semesterDb);
                    _context.SaveChanges();
                    ViewBag.DepartmentDeleteSuccessMessage = "Department deleted successfully";
                    return View("Index", _data);
                }

                ViewBag.DepartmentDeleteFailtureMessage = "Something went wrong. Department cannot be deleted";
                return View("Index", _data);
            }

            return View("Index", _data);
        }
    }
}