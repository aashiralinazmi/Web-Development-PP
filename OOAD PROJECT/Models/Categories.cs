using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public String CategoryName { get; set; }
    }
}