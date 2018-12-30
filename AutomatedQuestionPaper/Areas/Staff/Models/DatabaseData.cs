using System.Linq;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    /// <summary>
    ///     Provides most frequently information about database objects
    /// </summary>
    public class DatabaseData
    {
        /// EF Context for database
        private static readonly DatabaseContext Context = new DatabaseContext();

        /// <summary>
        ///     Provides semester information based on semester name
        /// </summary>
        /// <param name="semesterName">Name of semester</param>
        /// <returns>Semester</returns>
        public static Semester GetSemesterInfo(string semesterName)
        {
            var semester = Context.Semesters.FirstOrDefault(u => u.SemesterName == semesterName);
            return semester;
        }

        /// <summary>
        ///     Provides semester information based on department name
        /// </summary>
        /// <param name="departmentName">Name of department</param>
        /// <returns></returns>
        public static Department GetDepartmentInfo(string departmentName)
        {
            var department = Context.Departments.FirstOrDefault(u => u.DepartmentName == departmentName);
            return department;
        }

        /// <summary>
        ///     Provides subject information based on subject name
        /// </summary>
        /// <param name="subjectName">Name of subject</param>
        /// <returns>Subject</returns>
        public static Course GetCourseInfo(string subjectName)
        {
            var subject = Context.Courses.FirstOrDefault(u => u.CourseName == subjectName);
            return subject;
        }
    }
}