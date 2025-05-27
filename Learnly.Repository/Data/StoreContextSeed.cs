using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learnly.Repository.Data
{
    public class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            var departmentData = File.ReadAllText("../Learnly.Repository/Data/DataSeeding/departments.json");

            var departments = JsonSerializer.Deserialize<List<CourseDepartment>>(departmentData);

            if(departments.Count > 0)
            {
                if(_dbcontext.CourseDepartments.Count() == 0)  // to check if there is data in the departments or not
                {
                    foreach (var department in departments)
                    {
                        _dbcontext.Set<CourseDepartment>().Add(department);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }


            var categoryData = File.ReadAllText("../Learnly.Repository/Data/DataSeeding/categories.json");

            var categories = JsonSerializer.Deserialize<List<CourseCategory>>(categoryData);

            if (categories.Count > 0)
            {
                if (_dbcontext.CourseCategories.Count() == 0)  // to check if there is data in the categories or not
                {
                    foreach (var category in categories)
                    {
                        _dbcontext.Set<CourseCategory>().Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            var courseData = File.ReadAllText("../Learnly.Repository/Data/DataSeeding/courses.json");

            var courses = JsonSerializer.Deserialize<List<Course>>(courseData);

            if (courses.Count > 0)
            {
                if (_dbcontext.Courses.Count() == 0)  // to check if there is data in the courses or not
                {
                    foreach (var course in courses)
                    {
                        _dbcontext.Set<Course>().Add(course);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
