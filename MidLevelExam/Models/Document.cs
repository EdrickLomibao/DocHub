using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocHub.Models
{
    public class Document : AuditableEntity<long>
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string Text { get; set; }

    }
}