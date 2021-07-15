using System;
using System.Collections.Generic;
using System.Text;
using LibraryGUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryGUI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<BookEntry> BookEntries { get; set; }
        public DbSet<Owner> User { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Owner>(z =>
            //{
            //    z.HasOne(e => e.NormalizedUserName).WithOne()
            //        //.HasForeignKey(ue => ue.)
            //        .IsRequired();
            //});
         
        }


    }
}
