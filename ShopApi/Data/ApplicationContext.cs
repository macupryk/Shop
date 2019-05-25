using System;
using Microsoft.EntityFrameworkCore;
using ShopApi.Entities;

namespace ShopApi.Data
{
    public partial class ApplicationContext : BaseApplicationContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
              : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Item>().HasData(
            new { Id = 1L, Name = "Item1", Description = "Item1 Description", Price = 100, Stock = 10 ,CreatedDate=DateTime.UtcNow, ModifiedDate = DateTime.UtcNow },
            new { Id = 2L, Name = "Item2", Description = "Item2 Description", Price = 200, Stock = 5, CreatedDate = DateTime.UtcNow ,ModifiedDate = DateTime.UtcNow },
            new { Id = 3L, Name = "Item3", Description = "Item3 Description", Price = 300, Stock = 50, CreatedDate = DateTime.UtcNow,ModifiedDate = DateTime.UtcNow },
            new { Id = 4L, Name = "Item4", Description = "Item4 Description", Price = 250, Stock = 15, CreatedDate = DateTime.UtcNow , ModifiedDate = DateTime.UtcNow },
            new { Id = 5L, Name = "Item5", Description = "Item5 Description", Price = 400, Stock = 105, CreatedDate = DateTime.UtcNow , ModifiedDate = DateTime.UtcNow });
        }
    }

}
 
