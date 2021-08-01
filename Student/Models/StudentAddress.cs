namespace Student.Models
{
    public record StudentAddress:BaseEntity
    {
        public int StudentAddressID { get; set; } //defines the Id
        public int StudentID { get; set; } //define the foreign key from Student 
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual Student Student { get; set; }
    }
}