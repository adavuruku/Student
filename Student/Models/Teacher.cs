using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Student.Models
{
    public record Teacher:BaseEntity
    {
        public Teacher()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        
        public Nullable<int> TeacherType { get; set; }
    
        public Nullable<int> StandardId { get; set; }
        public virtual Standard Standard { get; set; }
        
        public virtual ICollection<Course> Courses { get; set; }
    }
}