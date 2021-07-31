namespace Student.Dto
{
    public class BodyHelper
    {
        public class AddStandardToLibrary
        {
            public int teacherId { get; set; }
            public int standardId { get; set; }
        }
        
        public class AddCourseToLibrary
        {
            public int teacherId { get; set; }
            public int courseId { get; set; }
        }
    }
}