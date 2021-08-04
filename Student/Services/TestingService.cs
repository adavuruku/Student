using System.Collections.Generic;
using System.Threading.Tasks;
using Student.Dto;
using Student.Models;

namespace Student.Services
{
    public interface TestingService
    {
       
        Task<IEnumerable<Models.Student>> GetAllStudent();
        
        
        Task<IEnumerable<Standard>> GetAllStandard();
        
        Task AddStudent(Models.Student student);
        Task AddStandard(Standard standard);
        Task<BodyHelper.LoginResponse> StudentLogin(BodyHelper.StudentLogin student);
        
        Task<Models.Student> LoadAStudent(int id);
       
        Task AddStudentAddress(StudentAddress studentAddress);
        
        Task AddGrade(Grade grade);
        Task<IEnumerable<Grade>> GetAllGrade();
        
        Task AddCourse(Course course);
        Task<IEnumerable<Course>> GetAllCourse();
        
        Task AddTeacher(Teacher teacher);
        Task<IEnumerable<Teacher>> GetAllTeacher();
        
        Task AddRole(Role role);
    }
}