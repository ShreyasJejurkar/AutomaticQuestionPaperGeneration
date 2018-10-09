using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    using System;

    public class CoursMetaData
    {

        [Key]
        [DisplayName("Course Id")]
        [Required]
        public int Courseid { get; set; }


        public Nullable<int> DepartmentId { get; set; }

        [DataType(DataType.Text)]
        [Required]
        [DisplayName("Course name")]
        public string CourseName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}