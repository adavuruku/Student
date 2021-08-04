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
        
        public class StudentLogin
        {
            public string studentPassword { get; set; }
            public string studentRegNo { get; set; }
        }
        
        public class LoginResponse
        {
            public string studentName { get; set; }
            public string studentRegNo { get; set; }
            public int studentId { get; set; }
            public string token { get; set; }
        }
        
        public class AddRoleToStudent
        {
            public int studentId { get; set; }
            public int roleId { get; set; }
        }
    }
}