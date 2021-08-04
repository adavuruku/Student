using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Student.Models
{
    //POCO entities (also known as persistence-ignorant objects)
    public record Student:BaseEntity
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Roles = new HashSet<Role>();
        }
    
        public int StudentID { get; set; }
        // [JsonIgnore]
        public string StudentPassword { get; set; }
        //[AllowNull]
        public string StudentRegNo { get; set; }
        public string StudentName { get; set; }
        
        public Nullable<int> StandardId { get; set; } // or public int? GradeId { get; set; } -> it means it can be null
        
        public byte[] RowVersion { get; set; }
    
        //reference navigation
        //property -> to fetch one items
        public virtual StudentAddress StudentAddress { get; set; }
        
        /// <summary>
        // one to many thats one student to many roles
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        
    }
}