namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    /// <summary>
    ///     Provides format for uploaded question files
    /// </summary>
    public class QuestionFormat
    {
        // The question text
        public string Question { get; set; }

        // The question difficulty level
        public int? Level { get; set; }

        public string Semester { get; set; }

        public string Department { get; set; }

        public int? Chapter { get; set; }

        public int? QuestionType { get; set; }

        public int? UnitId { get; set; }
    }
}