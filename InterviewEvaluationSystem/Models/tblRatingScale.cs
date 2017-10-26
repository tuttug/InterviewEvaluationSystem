//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterviewEvaluationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class tblRatingScale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblRatingScale()
        {
            this.tblScores = new HashSet<tblScore>();
        }
    
        public int RateScaleID { get; set; }
        [Required(ErrorMessage ="Please enter a Rate Scale ")]
        [Remote("IsScaleExist", "HR", AdditionalFields = "Id",
                ErrorMessage = "RateScale already exists")]
        public string RateScale { get; set; }
        [Required(ErrorMessage ="Please enter a Rate Value")]
        [Remote("IsValueExist", "HR", AdditionalFields = "Id",
              ErrorMessage = "RateValue already exists")]
        public int RateValue { get; set; }
        [Required(ErrorMessage ="Please enter a Description")]
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblScore> tblScores { get; set; }
    }
}
