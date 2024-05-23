using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Review1 { get; set; }

        public virtual Customer CUSTOMER { get; set; }
    }
}