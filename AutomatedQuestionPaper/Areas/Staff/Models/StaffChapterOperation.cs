using System;
using AutomatedQuestionPaper.Models;

namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class StaffChapterOperation : DatabaseData
    {
        private static readonly DatabaseContext Context = new DatabaseContext();

        public static void AddChapter(string selectedSemester, string selectedDepartment, string selectedSubject,
            string selectedUnit, string chapterNumber, string chapterName)
        {
            var semesterId = GetSemesterInfo(selectedSemester).Id;

            var departmentId = GetDepartmentInfo(selectedDepartment).Id;

            var subjectId = GetCourseInfo(selectedSubject).Courseid;

            Context.Chapters.Add(new Chapter()
            {
                ChapterName = chapterName,
                ChapterNo = Convert.ToInt32(chapterNumber),
                CourseId = subjectId,
                UnitNo = Convert.ToInt32(selectedUnit),
                DepartmentId = departmentId,
                SemesterId = semesterId
            });

            Context.SaveChangesAsync();
        }
    }
}