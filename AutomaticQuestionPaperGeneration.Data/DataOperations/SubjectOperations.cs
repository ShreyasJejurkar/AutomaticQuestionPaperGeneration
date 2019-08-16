using System.Collections.Generic;
using System.Linq;
using AutomaticQuestionPaperGeneration.Data.Models;

namespace AutomaticQuestionPaperGeneration.Data.DataOperations
{
    public static class SubjectOperations
    {
        /// <summary>
        /// Returns the list of Subjects
        /// </summary>
        /// <returns>List of Subjects</returns>
        public static IEnumerable<Subject> GetAllSubjects()
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Subjects.ToList();
            }
        }

        /// <summary>
        /// Return a single Subject matching with Id
        /// </summary>
        /// <param name="subjectId">Id of Subject</param>
        /// <returns></returns>
        public static Subject GetSubjectById(int subjectId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);
            }
        }

        /// <summary>
        /// Adds new Subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static bool CreateSubject(Subject subject)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                context.Subjects.Add(subject);
                return context.SaveChanges() == 1;
            }
        }

        /// <summary>
        /// Update existing record of Subject
        /// </summary>
        /// <param name="subjectId">Id of existing Subject record</param>
        /// <param name="subject">New Subject details</param>
        public static void UpdateSubject(int subjectId, Subject subject)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var subjectDb = context.Subjects.Single(x => x.SubjectId == subjectId);
                subjectDb.SubjectShortName = subject.SubjectShortName;
                subjectDb.SubjectYear= subject.SubjectYear;
                subjectDb.SubjectDescription= subject.SubjectDescription;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove the Subject
        /// </summary>
        /// <param name="subjectId">Id of Subject to be deleted</param>
        public static void DeleteSubject(int subjectId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var subjectDb = context.Subjects.FirstOrDefault(x => x.SubjectId == subjectId);

                if (subjectDb != null)
                {
                    context.Subjects.Remove(subjectDb);
                }

                context.SaveChanges();
            }
        }
    }
}
