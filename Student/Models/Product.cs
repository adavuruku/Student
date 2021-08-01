using System;

namespace Student.Models
{
    public record Product:BaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price{ get; set; }
        public DateTime DateCreated { get; set; }
    }
}