using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    /// <summary>
    /// Meta data type class for Staff
    /// </summary>
    public class StaffMetaData
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Please enter at least 5 characters")]
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Please enter at least 5 characters")]
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }

        [DataType(DataType.MultilineText)]
        [MinLength(5, ErrorMessage = "Please enter at least 5 characters")]
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MinLength(10, ErrorMessage = "Phone number cannot be less than 10 digit")]
        [MaxLength(10, ErrorMessage = "Phone number cannot be greater than 10 digit")]
        [Required]
        [DisplayName("Phone number")]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        [MinLength(5, ErrorMessage = "Email ID should be more than 5 characters")]
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should be greater than 8 characters")]
        [MaxLength(256, ErrorMessage = "Password should be less than 256 characters")]
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
