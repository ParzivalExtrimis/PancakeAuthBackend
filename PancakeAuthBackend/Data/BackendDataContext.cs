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
        }
    }
}
