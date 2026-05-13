using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Data.Context
{
    public  class SoftwareVersionManagerDbContext : DbContext
    {

       public SoftwareVersionManagerDbContext(DbContextOptions<SoftwareVersionManagerDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Software> Software { get; set; }
        public DbSet<SoftwareVersion> Versions { get; set; }

    }
}
