using Learnly.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository.Data.Configurations
{
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.Property(c => c.Description).IsRequired();

            builder.HasOne(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId);//.OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Department).WithMany().HasForeignKey(d => d.DepartmentId);

        }
    }
}
