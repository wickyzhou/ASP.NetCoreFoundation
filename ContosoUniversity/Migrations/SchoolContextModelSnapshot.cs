﻿// <auto-generated />
using System;
using ContosoUniversity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContosoUniversity.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContosoUniversity.Models.Course", b =>
                {
                    b.Property<int>("CourseID");

                    b.Property<int>("Credits")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(2);

                    b.Property<int>("DepartmentID");

                    b.Property<DateTime>("ModifyDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("ModifyTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<decimal>("MoneySecurity")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 38, scale: 17)))
                        .HasColumnType("money")
                        .HasDefaultValue(111111m);

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("CourseID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("ContosoUniversity.Models.CourseAssignment", b =>
                {
                    b.Property<int>("CourseID");

                    b.Property<int>("InstructorID");

                    b.HasKey("CourseID", "InstructorID");

                    b.HasIndex("InstructorID");

                    b.ToTable("CourseAssignment");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<int?>("InstructorID");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("DepartmentID");

                    b.HasIndex("InstructorID");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Instructor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("ContosoUniversity.Models.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID");

                    b.Property<string>("Location")
                        .HasMaxLength(50);

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignment");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EnrollmentDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName1")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Course", b =>
                {
                    b.HasOne("ContosoUniversity.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Models.CourseAssignment", b =>
                {
                    b.HasOne("ContosoUniversity.Models.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversity.Models.Instructor", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Models.Department", b =>
                {
                    b.HasOne("ContosoUniversity.Models.Instructor", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorID");
                });

            modelBuilder.Entity("ContosoUniversity.Models.Enrollment", b =>
                {
                    b.HasOne("ContosoUniversity.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversity.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Models.OfficeAssignment", b =>
                {
                    b.HasOne("ContosoUniversity.Models.Instructor", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("ContosoUniversity.Models.OfficeAssignment", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
