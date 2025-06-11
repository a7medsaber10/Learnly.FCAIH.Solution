using Learnly.Core.Enrollment_Aggregate;
using Learnly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository.Data
{
    public class StoreContext : DbContext
    {
        // Once I Created an object from StoreContext a Connection is Opened with Database.
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseDepartment> CourseDepartments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        
        public DbSet<EnrolledCourse> EnrolledCourses { get; set; }


    }
}
