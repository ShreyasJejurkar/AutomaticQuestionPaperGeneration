using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Department name")]
        [Required]
        public string DepartmentName { get; set; }
    }
}
