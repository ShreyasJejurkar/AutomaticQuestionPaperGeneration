using System.Collections.Generic;
using System.Linq;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    /// <summary>
    ///     Provides most frequently information about database objects
    /// </summary>
    public class DatabaseStaffOperations
    {
        // EF Context for database
        protected static readonly DatabaseContext Context = new DatabaseContext();
        protected readonly AutomatedQuestionPaper.Models.Staff Staff;

        public DatabaseStaffOperations(string staffName)
        {
            var staff = Context.Staffs.FirstOrDefault(u => u.Name == staffName);
            Staff = staff;
        }

        public AutomatedQuestionPaper.Models.Staff GetStaff()
        {
            return Staff;
        }

        /// <summary>
        ///     Returns the list of allocated semester to staff
        /// </summary>
        /// <returns>List of semester object</returns>
        public List<string> GetAllocatedSemesterNames()
        {
            var semesterIDs = Context.StaffCourses.Where(u => u.StaffId == Staff.Id)
                .Select(u => u.SemesterId).Distinct()
                .ToList();

            var semesterList = new List<string>();

            foreach (var id in semesterIDs)
            {
                var semester = Context.Semesters.FirstOrDefault(u => u.Id == id)?.SemesterName;
                semesterList.Add(semester);
            }

            return semesterList;
        }

        public List<string> GetAllocatedDepartment(string semester)
        {
            var semesterIDs = Context.Semesters.FirstOrDefault(u => u.SemesterName == semester)?.Id;

            var departmentIDs = Context.StaffCourses.Where(u => u.SemesterId == semesterIDs && u.StaffId == Staff.Id)
                .Select(u => u.DepartmentId).Distinct().ToList();

            var departmentNameList = new List<string>();

            foreach (var id in departmentIDs)
            {
                var departmentName = Context.Departments.FirstOrDefault(u => u.Id == id)?.DepartmentName;
                departmentNameList.Add(departmentName);
            }

            return departmentNameList;
        }

        public List<string> GetAllocatedSubjects(string semester, string department)
        {
            var deptId = Context.Departments.FirstOrDefault(u => u.DepartmentName == department)?.Id;

            var semId = Context.Semesters.FirstOrDefault(u => u.SemesterName == semester)?.Id;

            var subjectIDs = Context.StaffCourses.Where(u => u.DepartmentId == deptId && u.SemesterId == semId)
                .Select(u => u.CourseId).Distinct().ToList();

            var subjectsName = new List<string>();

            foreach (var id in subjectIDs)
            {
                var subjectName = Context.Courses.FirstOrDefault(u => u.Courseid == id)?.CourseName;
                subjectsName.Add(subjectName);

                // TODO can we make it inline like this
                // subjectsName.Add(Context.Courses.FirstOrDefault(u => u.Courseid == id)?.CourseName);
            }

            return subjectsName;
        }

        public List<ChapterDetails> GetChapters(string selectedSemester, string selectedDepartment,
            string selectedSubject)
        {
            var subjectId = Context.Courses.FirstOrDefault(u => u.CourseName == selectedSubject)?.Courseid;

            var departmentId = Context.Departments.FirstOrDefault(u => u.DepartmentName == selectedDepartment)?.Id;

            var semesterId = Context.Semesters.FirstOrDefault(u => u.SemesterName == selectedSemester)?.Id;

            var data = Context.Chapters.Where(u =>
                    u.SemesterId == semesterId && u.DepartmentId == departmentId && u.CourseId == subjectId)
                .Select(x => new {x.UnitNo, x.ChapterNo, x.ChapterName, x.SemesterId, x.CourseId, x.DepartmentId})
                .ToList();

            var list = data.Select(x => new ChapterDetails
            {
                UnitNo = x.UnitNo,
                ChapterName = x.ChapterName,
                ChapterNo = x.ChapterNo,
                SemesterId = x.SemesterId.ToString(),
                DepartmentId = x.DepartmentId.ToString(),
                SubjectId = x.CourseId.ToString()
            }).OrderBy(x => x.UnitNo).ToList();

            return list;
        }
    }
}