using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class ChapterMetaData
    {
        [Key] public int Id { get; set; }


        public int? CourseId { get; set; }

        public int? ChapterNo { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Chapter name")]
        [Required]
        public string ChapterName { get; set; }

        public int? UnitNo { get; set; }
        public int? SemesterId { get; set; }
        public int? DepartmentId { get; set; }
    }
}