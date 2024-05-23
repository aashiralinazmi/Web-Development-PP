using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public String ProductName { get; set; }
        public int Price { get; set; }
        public String Category { get; set; }
        public String Description { get; set; }
        public string Imagepath { get; set; }
        
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public String Stock { get; set; }
    }
}