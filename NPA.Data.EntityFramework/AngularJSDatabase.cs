using NPA.Business.Entities;
using System.Data.Entity;

namespace NPA.Data.EntityFramework
{
    public class AngularJSDatabase : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("dbo.Persons");
            modelBuilder.Entity<Product>().ToTable("dbo.Products");
        }
    }
}
