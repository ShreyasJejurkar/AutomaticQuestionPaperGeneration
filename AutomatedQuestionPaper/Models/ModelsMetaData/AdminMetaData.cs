using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{

    /// <summary>
    /// MetaData class for Admin class in Models
    /// </summary>
    public class AdminMetaData
    {
        [Key]
        public int id { get; set; }

        
        [DisplayName("Username")]
        [MaxLength(256)]
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }


        [DisplayName("Password")]
        [MinLength(8 , ErrorMessage = "The password must be of minimum 8 characters")]
        [MaxLength(256)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
