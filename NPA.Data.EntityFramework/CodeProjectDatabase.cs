﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPA.Business.Entities;
using System.Data.Entity;

namespace NPA.Data.EntityFramework
{
    /// <summary>
    /// CodeProject Entity Framework Database Context
    /// </summary>
    public class CodeProjectDatabase : DbContext
    {

        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }
      
        /// <summary>
        /// Model Creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().ToTable("dbo.Persons");
            modelBuilder.Entity<Product>().ToTable("dbo.Products");
         


        }
    }
}
