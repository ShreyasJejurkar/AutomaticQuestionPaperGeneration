using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutomatedQuestionPaper.Controllers;
using AutomatedQuestionPaper.DataAccessLayer;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Admin.Controllers
{
    [SessionCheckAdmin]
    public class DepartmentController : AlertController
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly List<Department> _data;

        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
            _data = _departmentRepository.GetAllDepartment().ToList();
        }

        
        [HttpGet]
        public JsonResult GetDepartmentJsonList()
        {
            var data =  Json(_departmentRepository.GetAllDepartment(), JsonRequestBehavior.AllowGet);
            return data;
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
        public ActionResult Create(Department dept)
        {
            _departmentRepository.AddDepartment(dept);

            Alert("Success", "Department added successfully",Enums.NotificationType.success);

            return RedirectToAction("Index", _data);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var department = _departmentRepository.GetDepartmentById(id);
            return View("Edit", department);
        }

        [HttpPost]
        public ActionResult Edit(Department dept)
        {
            _departmentRepository.UpdateDepartment(dept.Id,dept);
            
            Alert("Success", "Department edited successfully", Enums.NotificationType.success);

            return RedirectToActionPermanent("Index", _data);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                _departmentRepository.DeleteDepartment(id);

                Alert("Success", "Department deleted successfully", Enums.NotificationType.success);

                return View("Index", _data);
            }

            return View("Index", _data);
        }

        [HttpPost]
        public ActionResult DeleteMultipleDepartment(IEnumerable<int> selectedIds)
        {
            if (selectedIds == null)
            {
                Alert("Warning", "Select at least one department record to delete", Enums.NotificationType.warning);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var id in selectedIds)
                {
                    _departmentRepository.DeleteDepartment(id);
                }

                Alert("Successful", "Selected staff records deleted", Enums.NotificationType.success);
                return RedirectToAction("Index");
            }
        }
    }
}