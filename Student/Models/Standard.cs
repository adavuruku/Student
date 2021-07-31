using System.Collections.Generic;

namespace Student.Models
{
    public record Standard
    {
        public Standard()
        {
            this.Teachers = new HashSet<Teacher>();
        }
    
        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}