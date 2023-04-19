using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models;


namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductType> productsTypes{get;set;}

        public DbSet<TagesName> tagesName { get; set; }

        public DbSet<Products> products { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderDetails> orderDetails { get; set; }
    }
}
