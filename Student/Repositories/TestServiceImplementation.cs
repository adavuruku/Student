using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Student.Data;
using Student.Dto;
using Student.Models;
using Student.Services;

namespace Student.Repositories
{
    public class TestServiceImplementation: TestingService
    {
        private readonly IDataContext _context;
        
        //I'm inject Configuration service here so I can use my secret keys in appsetting.json to create webtoken
        private IConfiguration _configuration;
        
        public TestServiceImplementation(IDataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
        
        public async Task<BodyHelper.LoginResponse> StudentLogin(BodyHelper.StudentLogin student)
        {
            Console.WriteLine(student.studentRegNo + " , " +student.studentPassword);
            var studentLogin = await _context.Student
                .Where(s=> s.StudentPassword == student.studentPassword && s.StudentRegNo == student.studentRegNo)
                .Select(t=> new
                    BodyHelper.LoginResponse () {
                        studentName = t.StudentName, studentRegNo =  t.StudentRegNo, studentId = t.StudentID
                    }
                )
                .OrderBy(s=> s.studentName).FirstOrDefaultAsync();
            if (studentLogin is not  null)
            {
                studentLogin.token = generateJwtToken(studentLogin.studentId);
            }
            Console.WriteLine(studentLogin);
            return studentLogin;
        }

        public async Task<Models.Student> LoadAStudent(int id)
        {
            var itemRecord = await _context.Student.FindAsync(id);
           // Console.WriteLine(itemRecord.StudentName);
            return itemRecord;
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
        
        //JWT 
        
        
        //generating webtokens
        private string generateJwtToken(int studentId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
               /*
                claims are use to add information to the webtoken
                var claims = new[] {    
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),    
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),    
                new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),    
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())    
            };
                 Subject = new ClaimsIdentity(claims)
            */
               Audience =  _configuration["JWT:Issuer"],
               Issuer =  _configuration["JWT:Issuer"],
               Subject = new ClaimsIdentity(new[] { new Claim("id", studentId.ToString()) }),
               Expires = DateTime.UtcNow.AddDays(7),
               SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}