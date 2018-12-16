namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class QuestionFormatCsv
    {
        // The question text
        public string Question { get; set; }

        // The question difficulty level
        public int? Level { get; set; }
    }
}