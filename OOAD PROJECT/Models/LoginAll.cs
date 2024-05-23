using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OOAD_PROJECT.Models
{
    public class LoginAll
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Passward { get; set; }
        public string Roles { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}