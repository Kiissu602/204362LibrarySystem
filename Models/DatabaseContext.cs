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
        }

        internal Member FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
    
}
