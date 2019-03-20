using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace AutomatedQuestionPaper.Models
{
    [MetadataType(typeof(ExamPaperMetaData))]
    public partial class ExamPaper
    {

    }

    public partial class ExamPaperMetaData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Staff ID")]
        public Nullable<int> StaffId { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Paper name")]
        [Required]
        public string PaperName { get; set; }

        [DisplayName("Paper value")]
        [Required]
        public byte[] PaperValue { get; set; }
    }
}
