using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using Student.Services;

namespace Student.Repositories
{
    public class TestServiceImplementation: TestingService
    {
        private readonly IDataContext _context;
        public TestServiceImplementation(IDataContext context)
        {
            _context = context;
        }

        public async Task AddStudent(Models.Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Student>> GetAllStudent()
        {
            var allStudents = await _context.Student.ToListAsync();
            return allStudents;
        }

        public async Task AddStandard(Standard standard)
        {
            _context.Standard.Add(standard);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Standard>> GetAllStandard()
        {
            var allStandards = await _context.Standard.Include(s => s.Teachers).ToListAsync();
            return allStandards;
        }

        public async Task AddStudentAddress(StudentAddress studentAddress)
        {
            _context.StudentAddress.Add(studentAddress);
            await _context.SaveChangesAsync();
        }

        public async Task AddGrade(Grade grade)
        {
            _context.Grade.Add(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Grade>> GetAllGrade()
        {
            var allCourses = await _context.Grade.ToListAsync();
            return allCourses;
        }

        public async Task AddCourse(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourse()
        {
            var allCourses = await _context.Course.ToListAsync();
            return allCourses;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            _context.Teacher.Add(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeacher()
        {
            //var allTeachers = await _context.Teacher.Select(s => new {Course = s.Courses}).
             //   Include(s => s.Course).ToListAsync();
             
             var allTeachers = await _context.Teacher.Include(s => s.Courses).ToListAsync();
             if (allTeachers?.Any() == true)
             {
                 foreach (var rec in allTeachers)
                 {
                     
                 }
             }
            return allTeachers;
        }
    }
}