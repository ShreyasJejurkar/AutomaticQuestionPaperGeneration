using System.ComponentModel.DataAnnotations;
namespace AutomatedQuestionPaper.Models
{
    public enum YearList
    {
        [Display(Name = "Second year")]
        secondYear,

        [Display(Name = "Third year")]
        thirdYear,

        [Display(Name = "Fourth year")]
        fourthYear
    }
}