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

            modelBuilder.Entity<Student>()
               .HasIndex(p => p.Name);
        }
    }
}
