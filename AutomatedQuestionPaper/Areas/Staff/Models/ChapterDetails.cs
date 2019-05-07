namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class ChapterDetails
    {
        public int? UnitNo { get; set; }

        public int? ChapterNo { get; set; }

        public string ChapterName { get; set; }

        public string SemesterId { get; set; }

        public string DepartmentId { get; set; }

        public string SubjectId { get; set; }
    }
}