//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutomatedQuestionPaper.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Chapter
    {
        public int Id { get; set; }
        public Nullable<int> CourseId { get; set; }
        public Nullable<int> ChapterNo { get; set; }
        public string ChapterName { get; set; }
        public Nullable<int> UnitNo { get; set; }
        public Nullable<int> SemesterId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
    }
}
