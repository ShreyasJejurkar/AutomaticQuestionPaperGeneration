using System.Collections.Generic;
using System.Linq;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.DataAccessLayer
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseByName(string name)
        {
            return _context.Courses.FirstOrDefault(d => d.CourseName == name);
        }

        public Course GetCoursesById(int id)
        {
            return _context.Courses.FirstOrDefault(d => d.Courseid == id);
        }

        public IEnumerable<Course> GetAllCoursesByDepartment(Department dept)
        {
            return _context.Courses.Where(u => u.DepartmentId == dept.Id).ToList();
        }

        public void AddCourse(Course data)
        {
            _context.Courses.Add(data);

            Save();
        }

        public void DeleteCourse(int id)
        {
            var courseData = _context.Courses.FirstOrDefault(c => c.Courseid == id);
            _context.Courses.Remove(courseData);

            Save();
        }

        public void UpdateCourse(int id, Course data)
        {
            var oldCourseData = _context.Courses.FirstOrDefault(d => d.Courseid == id);
            oldCourseData.CourseName = data.CourseName;
            oldCourseData.CourseCode = data.CourseCode;
            oldCourseData.DepartmentId = data.DepartmentId;
            oldCourseData.Description = data.Description;
            oldCourseData.Year = data.Year;

            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        
    }
}