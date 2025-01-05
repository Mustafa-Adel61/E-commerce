using Ecommerce.core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.EF


{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Merchant> merchants { get; set; }
        public DbSet<Countrie> countries { get; set; }
        public DbSet<Cart> carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to many ralation User and Countrie and set countryCode Foreignkey
            modelBuilder.Entity<User>()
                .HasOne(c => c.countrie)
                .WithMany(c=>c.Users)
                .HasForeignKey(c => c.CountrieCode);
            //composite primary keys in orderItem Domain Model 
            modelBuilder.Entity<OrderItem>().HasKey(c => new { c.orderId, c.ProductId });

            modelBuilder.Entity<Merchant>()
                .HasOne(c => c.countrie)
                .WithMany(m=>m.merchants)
                .HasForeignKey(c => c.country_Code);
                
        }



    }
}
