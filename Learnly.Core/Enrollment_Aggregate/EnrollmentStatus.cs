using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Enrollment_Aggregate
{
    public enum EnrollmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "Cancelled")]
        Cancelled
    }
}
