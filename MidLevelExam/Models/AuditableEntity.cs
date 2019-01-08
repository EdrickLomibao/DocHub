using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DocHub.Models
{
    public abstract class AuditableEntity<T> : IAuditableEntity
    {
        [ScaffoldColumn(false)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        [StringLength(50)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } 

        [ScaffoldColumn(false)]
        [Display(Name = "Updated Date")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        [StringLength(50)]
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; } 

    }
}