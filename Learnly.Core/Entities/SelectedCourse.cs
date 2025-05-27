namespace Learnly.Core.Entities
{
    public class SelectedCourse
    {
        public int Id { get; set; } // here we don't deal with appDbContext 

        public string CourseName { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public string Department { get; set; }

        public string Category { get; set; }
    }
}