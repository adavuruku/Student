using System;
using System.Collections.Generic;

namespace Student.Models
{
    //POCO entities (also known as persistence-ignorant objects)
    public record Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> StandardId { get; set; } // or public int? GradeId { get; set; } -> it means it can be null
        public byte[] RowVersion { get; set; }
    
        //reference navigation property -> to fetch one items
        public virtual StudentAddress StudentAddress { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }
    }
}