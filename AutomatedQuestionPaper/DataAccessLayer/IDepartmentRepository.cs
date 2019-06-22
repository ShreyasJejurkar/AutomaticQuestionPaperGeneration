using System.Collections.Generic;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.DataAccessLayer
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartment();
        Department GetDepartmentById(int id);
        Department GetDepartmentByName(string name);
        void AddDepartment(Department data);
        void DeleteDepartment(int id);
        void UpdateDepartment(int id, Department data);
        void Save();
    }
}