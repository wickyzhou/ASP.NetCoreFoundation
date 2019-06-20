using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;

namespace ContosoUniversity
{
    public class SchoolContext : DbContext
    {
        public SchoolContext (DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 为每个实体集[表]创建 DbSet<TEntity> 属性。 实际上就是显示会话中的，可以使用哪些表。
        /// 每个属性都是一个表，表的结构又是通过模型来生成的，所以用DbSet<模型类名称>  【实体集合：TEntity】，作为表的类型。
        /// </summary>
        public DbSet<Student> Student { get; set; }

        //以下实体集合可以省略，因为Student引用了Enrollment,Enrollment 引用了Course;
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });//定义主键

            modelBuilder.Entity<Course>()
                .Property(m => m.Credits)
                .HasDefaultValue(2);

            modelBuilder.Entity<Course>().Property(m => m.ModifyTime).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Course>().Property(m => m.MoneySecurity).HasDefaultValue("111111");

            modelBuilder.Entity<Course>().Property(m => m.ModifyDate).HasColumnType("datetime").HasDefaultValueSql("getdate()");

        }

    }
}
