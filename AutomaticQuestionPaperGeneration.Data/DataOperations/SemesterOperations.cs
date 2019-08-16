using System.Collections.Generic;
using System.Linq;
using AutomaticQuestionPaperGeneration.Data.Models;

namespace AutomaticQuestionPaperGeneration.Data.DataOperations
{
    public static class SemesterOperations
    {
        /// <summary>
        /// Returns the list of Semesters
        /// </summary>
        /// <returns>List of Semesters</returns>
        public static IEnumerable<Semester> GetAllSemesters()
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Semesters.ToList();
            }
        }

        /// <summary>
        /// Return a single Semester matching with Id
        /// </summary>
        /// <param name="semesterId">Id of Semester</param>
        /// <returns></returns>
        public static Semester GetSemesterById(int semesterId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Semesters.FirstOrDefault(x => x.SemesterId == semesterId);
            }
        }

        /// <summary>
        /// Adds new Semester
        /// </summary>
        /// <param name="semester"></param>
        /// <returns></returns>
        public static bool CreateSemester(Semester semester)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                context.Semesters.Add(semester);
                return context.SaveChanges() == 1;
            }
        }

        /// <summary>
        /// Update existing record of Semester
        /// </summary>
        /// <param name="semesterId">Id of existing Semester record</param>
        /// <param name="semester">New Semester details</param>
        public static void UpdateSemester(int semesterId, Semester semester)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var semesterDb = context.Semesters.Single(x => x.SemesterId == semesterId);
                semesterDb.SemesterName = semester.SemesterName;
                semesterDb.SemesterStartDate = semester.SemesterStartDate;
                semesterDb.SemesterEndDate = semester.SemesterEndDate;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove the Semester
        /// </summary>
        /// <param name="semesterId">Id of Semester to be deleted</param>
        public static void DeleteSemester(int semesterId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var semesterDb = context.Semesters.FirstOrDefault(x => x.SemesterId == semesterId);

                if (semesterDb != null)
                {
                    context.Semesters.Remove(semesterDb);
                }

                context.SaveChanges();
            }
        }
    }
}
