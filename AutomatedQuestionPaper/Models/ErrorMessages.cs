using System.Collections.Generic;
namespace AutomatedQuestionPaper.Models
{
    public class ErrorMessages
    {
        public string ErrorText { get; set; }
    }

    public class ErrorMessagesList : List<ErrorMessages>
    {

    }
}