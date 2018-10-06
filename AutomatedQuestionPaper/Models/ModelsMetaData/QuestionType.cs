using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    using System;

    public class QuestionType
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        public Nullable<bool> IsActive { get; set; }
    }
}
