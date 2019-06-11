using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class DepartmentController : AlertController
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private readonly DbSet<Department> _data;

        public DepartmentController()
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

            Alert("Success", "Department added successfully",Enums.NotificationType.success);

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
            var semesterDb = _context.Departments.FirstOrDefault(u => u.Id == dep.Id);

            if (semesterDb != null)
                semesterDb.DepartmentName = dep.DepartmentName;

            _context.SaveChanges();
            
            Alert("Success", "Department edited successfully", Enums.NotificationType.success);

            return RedirectToActionPermanent("Index", _data);
        }

        [HttpGet]
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

                    Alert("Success", "Department deleted successfully", Enums.NotificationType.success);

                    return View("Index", _data);
                }

                
                Alert("Warning", "Something went wrong. Department cannot be deleted", Enums.NotificationType.error);
                return View("Index", _data);
            }

            return View("Index", _data);
        }

        [HttpPost]
        public ActionResult DeleteMultipleDepartment(IEnumerable<int> selectedIds)
        {
            if (selectedIds == null)
            {
                Alert("", "Select at least one department record to delete", Enums.NotificationType.warning);
                return RedirectToAction("Index");
            }
            else
            {
                _context.Departments.Where(x => selectedIds.Contains(x.Id))
                    .ToList().ForEach(p => _context.Departments.Remove(p));
                _context.SaveChanges();
                Alert("Successful", "Selected staff records deleted", Enums.NotificationType.success);
                return RedirectToAction("Index");
            }
        }
    }
}