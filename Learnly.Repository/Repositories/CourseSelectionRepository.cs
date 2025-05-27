using Learnly.Core.Entities;
using Learnly.Core.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learnly.Repository.Repositories
{
    public class CourseSelectionRepository : ICourseSelectionRepository
    {
        private readonly IDatabase _database;

        public CourseSelectionRepository(IConnectionMultiplexer redis) 
        {
            _database = redis.GetDatabase();    
        }
        public Task<bool> DeleteCourseSelectionAsync(string selectionId)
        {
            return _database.KeyDeleteAsync(selectionId);
        }

        public async Task<CourseSelection?> GetCourseSelectionAsync(string selectionId)
        {
            var courseSelection = await _database.StringGetAsync(selectionId);

            return  courseSelection.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CourseSelection>(courseSelection);
        }

        public async Task<CourseSelection?> UpdateCourseSelectionAsync(CourseSelection courseSelection) // add or update
        {
            var createdOrUpdatedCourseSelection =
                await _database.StringSetAsync(courseSelection.Id, JsonSerializer.Serialize(courseSelection), TimeSpan.FromDays(30));

            if (createdOrUpdatedCourseSelection is false) return null;

            return await GetCourseSelectionAsync(courseSelection.Id);
        }
    }
}
