namespace AutomatedQuestionPaper.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ModelsMetaData.StaffCourseMetaData))]
    public partial class StaffCourse
    {
        public int Id { get; set; }
        public Nullable<int> SemesterId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> CourseId { get; set; }
    }
}
