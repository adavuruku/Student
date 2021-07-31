namespace Student.Models
{
    public record Grade
    {
        public int GradeId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }
    }
}