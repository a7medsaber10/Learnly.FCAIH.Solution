using Learnly.Core.Enrollment_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Services.Contract
{
    public interface IEnrollmentServicee
    {
        Task<Enrollment?> CreateEnrollmentAsync(string studentEmail, string courseSelectionId);

        Task<IReadOnlyList<Enrollment>> GetEnrollmentsForStudentAsync(string studentEmail);

        Task<Enrollment> GetEnrollmentByIdForStudentAsync(int enrollmentId, string studentEmail);
    }
}
