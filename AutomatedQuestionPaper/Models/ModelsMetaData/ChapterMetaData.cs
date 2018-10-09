using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    using System;

    public class ChapterMetaData
    {
        [Key]
        public int Id { get; set; }


        public Nullable<int> CourseId { get; set; }

        public Nullable<int> ChapterNo { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Chapter name")]
        [Required]
        public string ChapterName { get; set; }
    }
}
