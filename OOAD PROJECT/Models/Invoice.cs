using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public String Payment { get; set; }
        public System.DateTime Date { get; set; }
        public int Status { get; set; }


        public virtual LoginAll LoginAll { get; set; }
    }
}