using System;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Data.Models;

namespace WebApiCore.Data.Context
{
   public class WebApiCoreContext : DbContext
{
    public DbSet<Customer> Customers {get;set;}

    public DbSet<CurrentWeather> Weathers {get; set; }

   public WebApiCoreContext(DbContextOptions<WebApiCoreContext> options) : base(options)
   {
      Database.Migrate();
   }
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Customer>().HasData(new Customer{Id = 1, nameof = "Nick", BirthDate = new DateTime(2000, 01, 01)});
      modelBuilder.Entity<Customer>().HasData(new Customer{Id = 2, nameof = "Loren", BirthDate = new DateTime(1980, 01, 01)});
      
      modelBuilder.Entity<CurrentWeather>().HasData(new CurrentWeather { Id = 1, Status ="Cloud", MinTemp = 20, MaxTemp = 22});
      modelBuilder.Entity<CurrentWeather>().HasData(new CurrentWeather { Id = 2, Status ="Clear", MinTemp = 18, MaxTemp = 20});
      }
}
}