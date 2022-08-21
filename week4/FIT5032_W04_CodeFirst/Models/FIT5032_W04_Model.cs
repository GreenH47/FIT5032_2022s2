using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace FIT5032_W04_CodeFirst.Models
{
    public class FIT5032_W04_Model : DbContext
    {
        // Your context has been configured to use a 'FIT5032_W04_Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FIT5032_W04_CodeFirst.Models.FIT5032_W04_Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'FIT5032_W04_Model' 
        // connection string in the application configuration file.
        public FIT5032_W04_Model()
            : base("name=FIT5032_W04_Model")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        // The generated DB name is Singular (Student instead of Students)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Student
    {
        public int ID { get; set; }// ID can be 'ID' or 'Class name + ID' as PK
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }



    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

    }

    public class Course
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}