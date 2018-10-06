using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(256)]
        [MinLength(5)]
        [Required]
        [DisplayName("Name")]
        public string SemesterName { get; set; }
    }
}
