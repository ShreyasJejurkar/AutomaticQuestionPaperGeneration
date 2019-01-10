namespace AutomatedQuestionPaper.Areas.Staff.Models
{
    public class PaperCreationQuestionFormat
    {
        // The question text
        public string Question { get; set; }

        // The question difficulty level
        public int? Level { get; set; }

        // unit no
        public int? Unit { get; set; }

    }
}