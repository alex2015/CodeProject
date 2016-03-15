using NPA.Business.Entities;
using System.Data.Entity;

namespace NPA.Data.EntityFramework
{
    public class AngularJSDatabase : DbContext
    {
        public DbSet<Person> Persons { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("dbo.Persons");
        }
    }
}
