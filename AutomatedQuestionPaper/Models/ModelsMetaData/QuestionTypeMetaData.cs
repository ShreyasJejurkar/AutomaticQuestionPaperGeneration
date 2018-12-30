using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class QuestionTypeMetaData
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        public bool? IsActive { get; set; }
    }
}