using Learnly.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learnly.Core.Repositories.Contract
{
    public interface ICourseSelectionRepository
    {
        Task<CourseSelection?> GetCourseSelectionAsync(string selectionId);

        Task<CourseSelection?> UpdateCourseSelectionAsync(CourseSelection courseSelection); // Add & Update

        Task<bool> DeleteCourseSelectionAsync(string selectionId);

    }
}
