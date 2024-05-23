using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class Customer
    {
        [Key]
        public int Customerid { get; set; }
        public string Customername { get; set; }
        public string UserName { get; set; }
        public string CustomerE_Mail { get; set; }
        public long CustomerPhoneNo { get; set; }
        public string Passward { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}