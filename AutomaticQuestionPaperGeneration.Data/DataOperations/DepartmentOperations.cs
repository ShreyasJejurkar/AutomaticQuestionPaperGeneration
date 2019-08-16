using System.Collections.Generic;
using System.Linq;
using AutomaticQuestionPaperGeneration.Data.Models;

namespace AutomaticQuestionPaperGeneration.Data.DataOperations
{
    /// <summary>
    /// Performs operation on Department table
    /// </summary>
    public static class DepartmentOperations
    {
        /// <summary>
        /// Returns the list of departments
        /// </summary>
        /// <returns>List of departments</returns>
        public static IEnumerable<Department> GetAllDepartments()
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Departments.ToList();
            }
        }

        /// <summary>
        /// Return a single department matching with Id
        /// </summary>
        /// <param name="departmentId">Id of department</param>
        /// <returns></returns>
        public static Department GetDepartmentById(int departmentId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Departments.FirstOrDefault(x=>x.DepartmentId == departmentId);
            }
        }

        /// <summary>
        /// Adds new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static bool CreateDepartment(Department department)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                context.Departments.Add(department);
                return context.SaveChanges() == 1;
            }
        }

        /// <summary>
        /// Update existing record of department
        /// </summary>
        /// <param name="departmentId">Id of existing department record</param>
        /// <param name="department">New department details</param>
        public static void UpdateDepartment(int departmentId, Department department)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var departmentDb = context.Departments.Single(x => x.DepartmentId == departmentId);
                departmentDb.DepartmentName = department.DepartmentName;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove the department
        /// </summary>
        /// <param name="departmentId">Id of department to be deleted</param>
        public static void DeleteDepartment(int departmentId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var departmentDb = context.Departments.FirstOrDefault(x => x.DepartmentId == departmentId);

                if (departmentDb != null)
                {
                    context.Departments.Remove(departmentDb);
                }

                context.SaveChanges();
            }
        }
    }
}
