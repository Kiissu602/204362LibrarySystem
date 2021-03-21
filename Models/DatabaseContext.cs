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
        public DbSet<Type> Type { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
             .HasOne(d => d.Faculty)
             .WithMany(f => f.Departmentlist);

            modelBuilder.Entity<Department>()
                    .HasOne(d => d.Faculty)
                    .WithMany(f => f.Departmentlist);

            modelBuilder.Entity<Member>()
                  .HasOne(u => u.Department)
                  .WithMany(d => d.MemberList);

            modelBuilder.Entity<Member>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

            modelBuilder.Entity<Type>()
                    .HasAlternateKey(t => t.TypeName);
        }

        internal Member FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
    
}
