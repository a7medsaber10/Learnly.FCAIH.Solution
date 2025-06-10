using Learnly.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository.Data.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            var adminRoleId = "DDC16163-28DC-4325-BECE-56A2B5BBE8E0"; // Fixed GUID
            var studentRoleId = "31C0AC99-29F4-4A55-AF0F-07402617FC47"; // Fixed GUID
            var teacherRoleId = "C1DA0795-351E-45E3-8C4F-74DA1438BB50"; // Fixed GUID

            //var adminUserId = "0BE7B103-1D31-420F-853C-EE3BC9236FB4"; // Fixed GUID

            var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = studentRoleId, Name = "Student", NormalizedName = "STUDENT" },
                new IdentityRole { Id = teacherRoleId, Name = "Teacher", NormalizedName = "TEACHER" }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Create admin user
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Admin@123";

            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                //Id = adminUser.Id,
                UserName = adminEmail,
                NormalizedUserName = adminEmail.ToUpper(),
                Email = adminEmail,
                NormalizedEmail = adminEmail.ToUpper(),
                EmailConfirmed = true,
                DisplayName = "System Admin",
                PasswordHash = hasher.HashPassword(null, adminPassword)
            };
            builder.Entity<AppUser>().HasData(adminUser);


            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = adminUser.Id,
                RoleId = adminRoleId
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);



            base.OnModelCreating(builder);

            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}
