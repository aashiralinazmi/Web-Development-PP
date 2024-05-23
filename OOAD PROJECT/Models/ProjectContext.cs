using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OOAD_PROJECT.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<Products> Product { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoginAll> LoginAlls { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Order> orders { get; set; }
    }
}