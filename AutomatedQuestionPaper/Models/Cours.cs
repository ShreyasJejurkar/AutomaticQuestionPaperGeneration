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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ModelsMetaData.CoursMetaData))]
    public partial class Cours
    {
        public int Courseid { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
    }
}
