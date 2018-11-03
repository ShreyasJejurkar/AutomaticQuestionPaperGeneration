using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class DepartmentController : Controller
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Department> _data;

        public DepartmentController() : this(1)
        {
        }

        public DepartmentController(int data)
        {
            _data = _context.Departments;
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

        /// <summary>
        ///     Action will create the department entry in Department table
        /// </summary>
        /// <param name="dept">Department object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Department dept)
        {
            // Add to the context and save it to database
            _context.Departments.Add(dept);
            _context.SaveChanges();

            // Create success message and pass it to view
            TempData["DepartmentAddedSuccessMessage"] = "Department added successfully";

            return RedirectToAction("Index", _data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Get the requested department from table as per id
            var department = _context.Departments.FirstOrDefault(u => u.Id == id);

            return View("Edit", department);
        }

        /// <summary>
        ///     Action will response for POST method and will perform department edit operation
        /// </summary>
        /// <param name="dep">Edited department object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Department dep)
        {
            // Get the old details of department from table
            var semesterDb = _context.Departments.FirstOrDefault(u => u.Id == dep.Id);

            // Set the new details of department
            if (semesterDb != null)
                semesterDb.DepartmentName = dep.DepartmentName;

            // Save the changes and commit it to database
            _context.SaveChanges();
            TempData["DepartmentDeleteSuccessMessage"] = "Department edited successfully";

            return RedirectToActionPermanent("Index", _data);
        }


        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                //Get the details of department first from database
                var departmentDb = _context.Departments.SingleOrDefault(u => u.Id == id);

                if (departmentDb != null)
                {
                    //Delete its details and commit the operation
                    _context.Departments.Remove(departmentDb);
                    _context.SaveChanges();

                    //Set the success message
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