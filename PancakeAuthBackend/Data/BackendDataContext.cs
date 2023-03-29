using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Models;

namespace PancakeAuthBackend.Data {
    public class BackendDataContext : DbContext {
        public BackendDataContext(DbContextOptions options) : base(options) {
        }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            //impose unique contraints
            
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


            //school model
            modelBuilder.Entity<School>()
             .HasIndex(school => school.Name)
             .IsUnique();

            //subject model
            modelBuilder.Entity<Subject>()
             .HasIndex(subject => subject.Name)
             .IsUnique();

            //subscription model
            modelBuilder.Entity<Subscription>()
             .HasIndex(subscription => subscription.Name)
             .IsUnique();

            //Grade model
            modelBuilder.Entity<Grade>()
            .HasIndex(grade => grade.Name)
            .IsUnique();

            //Chapter Model
            modelBuilder.Entity<Chapter>()
            .HasIndex(chapter => chapter.Title)
            .IsUnique();
        }
    }
}
