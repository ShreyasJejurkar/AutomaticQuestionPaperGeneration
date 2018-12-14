namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    /// <summary>
    /// Provides format for uploaded question files
    /// </summary>
    public class QuestionFormat
    {
        // The question text
        public string Question { get; set; }

        // The question difficulty level
        public int? Level { get; set; }
    }
}