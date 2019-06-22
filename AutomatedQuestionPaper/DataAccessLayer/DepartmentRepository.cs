using System.Collections.Generic;
using System.Linq;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.DataAccessLayer
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public IEnumerable<Department> GetAllDepartment()
        {
            return _context.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public Department GetDepartmentByName(string name)
        {
            return _context.Departments.FirstOrDefault(d => d.DepartmentName == name);
        }

        public void AddDepartment(Department data)
        {
            data.DepartmentName += "department";
            _context.Departments.Add(data);

            Save();
        }

        public void DeleteDepartment(int id)
        {
            var departmentData = _context.Departments.FirstOrDefault(d => d.Id == id);
            _context.Departments.Remove(departmentData);

            Save();
        }

        public void UpdateDepartment(int id, Department data)
        {
            var oldDepartmentData = _context.Departments.FirstOrDefault(d => d.Id == id);
            oldDepartmentData.DepartmentName = data.DepartmentName;

            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}