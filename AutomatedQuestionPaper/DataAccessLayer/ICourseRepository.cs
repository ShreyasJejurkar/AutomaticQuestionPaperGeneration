using System.Collections.Generic;
using AutomatedQuestionPaper.Models;
namespace AutomatedQuestionPaper.DataAccessLayer
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        IEnumerable<Course> GetAllCoursesByDepartment(Department dept);
        Course GetCoursesById(int id);
        Course GetCourseByName(string name);
        void AddCourse(Course data);
        void DeleteCourse(int id);
        void UpdateCourse(int id, Course data);
        void Save();
    }
}