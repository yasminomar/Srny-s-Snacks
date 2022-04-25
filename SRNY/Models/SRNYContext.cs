using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SRNY.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<Cart> Cart { get; set; }
        public virtual List<Reviews> Reviews { get; set; }



    }
    public class SRNYContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public SRNYContext() : base()
        {

        }
        public SRNYContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<CategoriesInProduct> CategoriesInProduct { get; set; }
        public DbSet<ProductInCart> ProductInCart { get; set; }
        public DbSet<Shipping> Shipping { get; set; }






    }
}
