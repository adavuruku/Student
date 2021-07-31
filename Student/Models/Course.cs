using System.Collections.Generic;

namespace Student.Models
{
    public record  Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
    
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Location { get; set; }
    
        public virtual ICollection<Student> Students { get; set; }
    }
}