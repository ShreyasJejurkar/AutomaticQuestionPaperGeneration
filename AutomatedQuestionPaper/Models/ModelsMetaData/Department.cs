using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    {

    }
    
    public class DepartmentMetaData
    {
        [Key] public int Id { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Department name")]
        [Required]
        public string DepartmentName { get; set; }
    }
}