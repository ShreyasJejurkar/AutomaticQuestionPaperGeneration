using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class CourseMetaData
    {
        [Key]
        [DisplayName("Subject Id")]
        [Required]
        public int Courseid { get; set; }

        [DisplayName("Department name")] public int? DepartmentId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Subject name")]
        public string CourseName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required] [DisplayName("Year")] public string Year { get; set; }

        [Required]
        [DisplayName("Subject code")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid subject code")]
        public string CourseCode { get; set; }
    }
}