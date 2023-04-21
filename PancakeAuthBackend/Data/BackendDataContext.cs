﻿using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PancakeAuthBackend.Models;

namespace PancakeAuthBackend.Data {
    public class BackendDataContext : DbContext {
        public BackendDataContext(DbContextOptions options) : base(options) {
        }
        public DbSet<Billing> Payments { get; set; }
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
              .HasOne(s => s.Batch)
              .WithMany(s => s.Students)
              .HasForeignKey(s => s.SchoolId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
             .HasOne(s => s.Batch)
             .WithMany(s => s.Students)
             .HasForeignKey(s => s.BatchId)
             .OnDelete(DeleteBehavior.Cascade);


            //school model
            modelBuilder.Entity<School>()
             .HasIndex(school => school.Name)
             .IsUnique();

            modelBuilder.Entity<School>()
            .HasMany(sch => sch.Payments)
            .WithOne(s => s.School)
            .HasForeignKey(s => s.SchoolId)
            .OnDelete(DeleteBehavior.NoAction);

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

            //subscription model
            modelBuilder.Entity<Subscription>()
             .HasIndex(subscription => subscription.Name)
             .IsUnique();

            modelBuilder.Entity<Subscription>()
              .HasMany(sub => sub.Schools)
              .WithMany(school => school.Subscriptions)
              .UsingEntity<AvailedSubscription>();

            //Grade model
            modelBuilder.Entity<Grade>()
            .HasIndex(grade => grade.Name)
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

            //sample data
            //SampleDataSeeder.Seed(modelBuilder);
        }
    }
}
