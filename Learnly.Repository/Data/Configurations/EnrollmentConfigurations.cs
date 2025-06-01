using Learnly.Core.Enrollment_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Repository.Data.Configurations
{
    public class EnrollmentConfigurations : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.Property(e => e.Status).HasConversion
            (
                EStatus => EStatus.ToString(),
                EStatus => (EnrollmentStatus) Enum.Parse(typeof(EnrollmentStatus), EStatus)
            );
        }
    }
}
