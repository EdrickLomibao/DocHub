using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DocHub.Models
{
    public class DBContext : IdentityDbContext<ApplicationUser>
    {
        public DBContext() : base("name=DefaultConnection") {

        }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().ToTable("Account").Property(p => p.Id).HasColumnName("ID");
            // modelBuilder.Entity<IdentityRole>().ToTable("Account").Property(p => p.Id).HasColumnName("ID");

        }

    }
}