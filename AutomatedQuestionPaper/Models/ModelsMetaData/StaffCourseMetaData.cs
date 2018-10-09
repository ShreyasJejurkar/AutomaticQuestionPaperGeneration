using System.ComponentModel.DataAnnotations;
using System;
namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class StaffCourseMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Nullable<int> SemesterId { get; set; }

        [Required]
        public Nullable<int> StaffId { get; set; }

        [Required]
        public Nullable<int> CourseId { get; set; }
    }
}
