using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using PancakeAuthBackend.Models;

namespace PancakeAuthBackend.Data {
    public class BackendDataContext : IdentityDbContext<User> {
        public BackendDataContext() { }
        public BackendDataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Billing> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);


            //impose unique contraints and ON DELETE constriants

            //student model
            modelBuilder.Entity<Student>()
               .HasIndex(student => student.StudentUID)
               .IsUnique();

            modelBuilder.Entity<Student>()
              .HasIndex(student => student.Email)
              .IsUnique();

            modelBuilder.Entity<Student>()
              .HasIndex(student => student.PhoneNumber)
              .IsUnique();

            modelBuilder.Entity<Student>()
              .HasOne(s => s.School)
              .WithMany(s => s.Students)
              .HasForeignKey(s => s.SchoolId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
             .HasMany(s => s.Billings)
             .WithOne(b => b.Student)
             .HasForeignKey(b => b.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
            .HasOne(s => s.Department)
            .WithMany(s => s.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);


            //school model
            modelBuilder.Entity<School>()
             .HasIndex(school => school.Name)
             .IsUnique();

            modelBuilder.Entity<School>()
              .HasOne(sch => sch.Address)
              .WithOne(s => s.School)
              .OnDelete(DeleteBehavior.Cascade);

            //subject model
            modelBuilder.Entity<Subject>()
             .HasIndex(subject => subject.Name)
             .IsUnique();

            modelBuilder.Entity<Subject>()
            .HasMany(sub => sub.Chapters)
            .WithOne(chap => chap.Subject)
            .HasForeignKey(c => c.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

            //Department model
            modelBuilder.Entity<Department>()
            .HasIndex(d => d.Name)
            .IsUnique();

            //Chapter Model
            modelBuilder.Entity<Chapter>()
            .HasIndex(chapter => chapter.Title)
            .IsUnique();

            modelBuilder.Entity<Chapter>()
             .HasOne(ch => ch.Subject)
             .WithMany(sub => sub.Chapters)
             .HasForeignKey(chap => chap.SubjectId)
             .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
