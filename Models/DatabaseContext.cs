using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Member> Member { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
             .HasOne(f => f.Faculty)
             .WithMany(d => d.Departmentlist);

            modelBuilder.Entity<Member>()
                  .HasOne(d => d.Department)
                  .WithMany(m => m.MemberList);

            modelBuilder.Entity<Member>()
                  .HasOne(j => j.Job)
                  .WithMany(m => m.MemberList);

            modelBuilder.Entity<Member>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

            modelBuilder.Entity<Job>()
                    .HasAlternateKey(t => t.JobName);

            modelBuilder.Entity<Book>()
                    .HasOne(b => b.Category)
                    .WithMany(c => c.Booklist);

            modelBuilder.Entity<Author>()
                    .HasKey(a => new { a.ISBN, a.WriterID });

            modelBuilder.Entity<Author>()
                    .HasOne(a => a.Book)
                    .WithMany(b => b.Authorlist)
                    .HasForeignKey(a => a.ISBN);

            modelBuilder.Entity<Author>()
                    .HasOne(a => a.Writer)
                    .WithMany(b => b.Authorlist)
                    .HasForeignKey(a => a.WriterID);
        }
    }
    
}
