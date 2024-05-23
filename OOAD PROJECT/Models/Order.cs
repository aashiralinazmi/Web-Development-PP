using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ProductId { get; set; }
        public long ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int SingleProductPrice { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }
        public int Subtotal { get; set; }
        public System.DateTime Date { get; set; }
        public int invoiceid { get; set; }
    }
}