using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models
{
    [MetadataType(typeof(SemesterMetaData))]
    public partial class Semester
    {

    }
    
    public class SemesterMetaData
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(256)]
        [MinLength(5)]
        [Required]
        [DisplayName("Name")]
        public string SemesterName { get; set; }
    }
}