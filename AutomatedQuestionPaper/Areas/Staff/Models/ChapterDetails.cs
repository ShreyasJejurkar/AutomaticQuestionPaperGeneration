namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    /// <summary>
    /// Represents a chapter
    /// </summary>
    public class ChapterDetails
    {
        /// <summary>
        /// Unit no 1 - 6
        /// </summary>
        public int? UnitNo { get; set; }

        /// <summary>
        /// A chapter no
        /// </summary>
        public int? ChapterNo { get; set; }

        /// <summary>
        /// Chapter name
        /// </summary>
        public string ChapterName { get; set; }

        public string SemesterId { get; set; }

        public string DepartmentId { get; set; }


        public string SubjectId { get; set; }
    }
}