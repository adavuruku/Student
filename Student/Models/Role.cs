using System.Collections.Generic;

namespace Student.Models
{
    public record Role:BaseEntity
    {
        public Role()
        {
            this.Students = new HashSet<Student>();
        }
        public int roleId { get; set; }
        public string roleTitle { get; set; }
        
        //define one to many  one role to many student
        //collection navigation
        public virtual ICollection<Student> Students { get; set; }
    }
}