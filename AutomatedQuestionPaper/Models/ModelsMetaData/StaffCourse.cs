using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models
{
    [MetadataType(typeof(StaffCourseMetaData))]
    public partial class StaffCourse
    {

    }
    
    public class StaffCourseMetaData
    {
        [Key] public int Id { get; set; }

        [Required] public int? SemesterId { get; set; }

        [Required] public int? StaffId { get; set; }

        [Required] public int? CourseId { get; set; }
    }
}