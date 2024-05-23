using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Cart
    {
        [Key]
        public int CardId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int SingleProductPrice { get; set; }

        public int Qunatity { get; set; }

        public int Subtotal { get; set; }
    }
}