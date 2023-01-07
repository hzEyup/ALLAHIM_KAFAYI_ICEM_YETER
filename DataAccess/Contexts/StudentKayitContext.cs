#nullable disable
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class StudentKayitContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<StudentLesson> studentLessons { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public StudentKayitContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentLesson>().HasKey(ps => new { ps.StudentId, ps.LessonId });

            modelBuilder.Entity<Student>()
                .HasOne(p => p.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(p => p.ClassId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentLesson>()
          .HasOne(ps => ps.Student)
          .WithMany(p => p.studentLessons)
          .HasForeignKey(ps => ps.StudentId)
          .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentLesson>()
               .HasOne(ps => ps.Lesson)
               .WithMany(p => p.StudentLessons)
               .HasForeignKey(ps => ps.LessonId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
              .HasOne(ud => ud.User)
              .WithOne(u => u.UserDetail)
              .HasForeignKey<UserDetail>(ud => ud.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.Country)
                .WithMany(c => c.UserDetails)
                .HasForeignKey(ud => ud.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserDetail>()
                .HasOne(ud => ud.City)
                .WithMany(c => c.UserDetails)
                .HasForeignKey(ud => ud.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                .HasOne(ci => ci.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(ci => ci.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<Student>()
               .HasIndex(p => p.Name);



        }
    }
}
