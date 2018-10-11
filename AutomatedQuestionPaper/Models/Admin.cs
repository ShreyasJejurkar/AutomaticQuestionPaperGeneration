using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models
{
    [MetadataType(typeof(ModelsMetaData.AdminMetaData))]
    public partial class Admin
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
