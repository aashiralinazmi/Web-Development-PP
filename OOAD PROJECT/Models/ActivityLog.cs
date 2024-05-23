using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class ActivityLog
    {
        [Key]
        public int Id { get; set; }
        public string Activity { get; set; }
        public string columnName { get; set; }
    }
}