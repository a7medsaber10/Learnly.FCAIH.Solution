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
    public class DepartmentConfigurations : IEntityTypeConfiguration<CourseDepartment>
    {
        public void Configure(EntityTypeBuilder<CourseDepartment> builder)
        {
            builder.Property(d => d.Name).IsRequired();
        }
    }
}
