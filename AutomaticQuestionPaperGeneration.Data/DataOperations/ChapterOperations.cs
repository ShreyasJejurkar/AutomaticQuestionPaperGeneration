using System.Collections.Generic;
using System.Linq;
using AutomaticQuestionPaperGeneration.Data.Models;

namespace AutomaticQuestionPaperGeneration.Data.DataOperations
{
    public static class ChapterOperations
    {
        /// <summary>
        /// Returns the list of Chapters
        /// </summary>
        /// <returns>List of Chapters</returns>
        public static IEnumerable<Chapter> GetAllChapters()
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Chapters.ToList();
            }
        }

        /// <summary>
        /// Return a single Chapter matching with Id
        /// </summary>
        /// <param name="chapterId">Id of Chapter</param>
        /// <returns></returns>
        public static Chapter GetChapterById(int chapterId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                return context.Chapters.FirstOrDefault(x => x.ChapterId == chapterId);
            }
        }

        /// <summary>
        /// Adds new Chapter
        /// </summary>
        /// <param name="chapter"></param>
        /// <returns></returns>
        public static bool CreateChapter(Chapter chapter)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                context.Chapters.Add(chapter);
                return context.SaveChanges() == 1;
            }
        }

        /// <summary>
        /// Update existing record of Chapter
        /// </summary>
        /// <param name="chapterId">Id of existing Chapter record</param>
        /// <param name="chapter">New Chapter details</param>
        public static void UpdateChapter(int chapterId, Chapter chapter)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var chapterDb = context.Chapters.Single(x => x.ChapterId == chapterId);
                chapterDb.ChapterName= chapter.ChapterName;
                chapterDb.ChapterNo= chapter.ChapterNo;
                chapterDb.ChapterUnit= chapter.ChapterUnit;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Remove the Chapter
        /// </summary>
        /// <param name="chapterId">Id of Chapter to be deleted</param>
        public static void DeleteChapter(int chapterId)
        {
            using (var context = new AutomaticQuestionPaperContext())
            {
                var chapterDb = context.Chapters.FirstOrDefault(x => x.ChapterId == chapterId);

                if (chapterDb != null)
                {
                    context.Chapters.Remove(chapterDb);
                }

                context.SaveChanges();
            }
        }
    }
}
