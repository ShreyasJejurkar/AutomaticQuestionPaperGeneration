using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedQuestionPaper.Models.ModelsMetaData
{
    using System;
    using System.Collections.Generic;

    public class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Answers = new HashSet<Answer>();
        }


        [Key]
        public int Id { get; set; }

        [DisplayName("Chapter Id")]
        [Required]
        public Nullable<int> ChapterId { get; set; }

        [DisplayName("Question Type Id")]
        [Required]
        public Nullable<int> QuestionTypeId { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }


        public Nullable<bool> IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
