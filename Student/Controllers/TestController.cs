using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Student.Data;
using Student.Dto;
using Student.Models;
using Student.MyJWT;
using Student.Services;

namespace Student.Controllers
{
    [ApiController]
    [Route("test")]
    //[Route("[controller]/[action]")] -> will use the controller name [the name before controller] combine with action name for the routes i.e test/createstandard
    public class TestController:ControllerBase
    {
        private readonly TestingService _testingService;
        private readonly IDataContext _context;
        
        public TestController(TestingService testingService, IDataContext context)
        {
            _testingService = testingService;
            _context = context;
        }

        [HttpPost]
       // [HttpPost("/tess/tt")]
        //[Route("[controller]")] -> will use the controller name [the name before controller] name test/test
       //[Route("[action]")] -> will use the action name -> test/test
        [Route("/new/standard")]
        public async Task<ActionResult<Standard>> CreateStandard(StandardDto standardDto)
        {
            Standard standard = new()
            {
                StandardName = standardDto.StandardName,
                Description = standardDto.Description
            };

            await _testingService.AddStandard(standard);
            return Ok(standard);
            //return CreatedAtAction(nameof(GetAllStandard), standard);
            //return CreatedAtAction(nameof(GetAProduct), new {id = productY.ProductId}, productY());
        }
        
        [HttpGet]
        [Route("/all/standard")]
        public async Task<ActionResult<Standard>> GetAllStandard()
        {
            var standard = await _testingService.GetAllStandard();
            return Ok(standard);
        }
        
        [HttpPost]
        [Route("/new/student")]
        public async Task<ActionResult<Models.Student>> CreateStudent(Models.Student student)
       // public async Task<ActionResult<Models.Student>> CreateStudent(StudentDto studentDto)
        {
           /* Models.Student student = new()
            {
                StudentName = studentDto.StudentName,
                StandardId = studentDto.StandardId
            };*/

            await _testingService.AddStudent(student);
            return Ok(student);
            //return CreatedAtAction(nameof(GetAllStudent), student);
        }
        
        [HttpGet]
        [Route("/all/student")]
        
        public async Task<ActionResult<Standard>> GetAllStudent()
        {
            var students = await _testingService.GetAllStudent();
            return Ok(students);
            //return Json(students);
        }
        
        [HttpPost("/student/login")]
        public async Task<ActionResult<BodyHelper.LoginResponse>> StudentLogin(BodyHelper.StudentLogin student)
        {
            var loginResponse = await _testingService.StudentLogin(student);
            if (loginResponse is null)
            {
                return NoContent();
            }
            return Ok(loginResponse);
        }
        /*
         this will not give you access to the password since it has [JsonIgnore] in the model and
         such I have to create a structure for it
         public async Task<ActionResult<BodyHelper.LoginResponse>> StudentLogin(Models.Student student)
        {
            var loginResponse = await _testingService.StudentLogin(student);
            if (loginResponse is null)
            {
                return NoContent();
            }
            return Ok(loginResponse);
        }*/
        
        [HttpPost]
        [Route("/new/teacher")]
        public async Task<ActionResult<Models.Student>> CreateTeacher(Teacher teacher)
        {
            Teacher teach = new()
            {
                TeacherName = teacher.TeacherName,
                //StandardId = studentDto.StandardId
            };

            await _testingService.AddTeacher(teach);
            return Ok(teach);
            //return CreatedAtAction(nameof(GetAllStudent), student);
        }
        
        [HttpGet]
        [Authorize("Sherif",new string[] {"adams", "john", "paul"})]
        [Route("/all/teacher")]
        //[Authorize("Ahmad")]
        public async Task<ActionResult<Standard>> GetAllTeacher()
        {
            var allTeacher = await _testingService.GetAllTeacher();
            return Ok(allTeacher);
        }
        
        [HttpPost]
        [Route("/new/course")]
        public async Task<ActionResult<Models.Student>> CreateCourse(Course course)
        {
            Course cour = new()
            {
                CourseName = course.CourseName,
                Location = course.Location
            };

            await _testingService.AddCourse(cour);
            return Ok(cour);
            //return CreatedAtAction(nameof(GetAllStudent), student);
        }
        
        [HttpGet]
        [Route("/all/course")]
        public async Task<ActionResult<Standard>> GetAllCourse()
        {
            var allCourse = await _testingService.GetAllCourse();
            return Ok(allCourse);
        }
        
        
        [HttpPost]
        [Route("/new/grade")]
        public async Task<ActionResult<Models.Student>> CreateGrade(Grade grade)
        {
            Grade gra = new()
            {
                GradeName = grade.GradeName,
                Section = grade.GradeName,
                Score = grade.Score
            };

            await _testingService.AddGrade(gra);
            return Ok(gra);
            //return CreatedAtAction(nameof(GetAllStudent), student);
        }
        
        [HttpGet]
        [Route("/all/grade")]
        public async Task<ActionResult<Standard>> GetAllGrade()
        {
            var allGrade = await _testingService.GetAllGrade();
            return Ok(allGrade);
        }
        
        [HttpPatch]
        [Route("/teacher/add/standard")]
        public async Task<ActionResult<Standard>> AddStandardToTeacher([FromBody] BodyHelper.AddStandardToLibrary recordToUpdate)
        {
           // Console.WriteLine(recordToUpdate);
            //JObject json = JObject.FromObject(recordToUpdate);
            //JSON json = JsonConvert.SerializeObject(recordToUpdate, Formatting.Indented);
           // dynamic json = JObject(recordToUpdate);
            var existingStandard = await _context.Standard.FindAsync(recordToUpdate.standardId);
            var existingTeacher = await _context.Teacher.FindAsync(recordToUpdate.teacherId);
            if (existingStandard is null || existingTeacher is null)
            {
                return NotFound();
            }

            existingTeacher.StandardId = existingStandard.StandardId;
            
            await _context.SaveChangesAsync();
          
            //string json = MyJSONFormatter(existingTeacher);
            return Ok(existingTeacher);
        }
        
        [HttpPatch]
        [Route("/teacher/add/course")]
        public async Task<ActionResult<Standard>> AddCourseToTeacher([FromBody] BodyHelper.AddCourseToLibrary recordToUpdate)
        {
            var existingCourse = await _context.Course.FindAsync(recordToUpdate.courseId);
            var existingTeacher = await _context.Teacher.FindAsync(recordToUpdate.teacherId);
            if (existingCourse is null || existingTeacher is null)
            {
                return NotFound();
            }

            existingTeacher.Courses.Add(existingCourse);
            await _context.SaveChangesAsync();
          
            //string json = MyJSONFormatter(existingTeacher);
            return Ok(existingTeacher);
        }
        
        [HttpGet]
        [Route("/teacher/{teacherId}")]
        public async Task<ActionResult<Standard>> FindATeacher(int teacherId)
        {
           // var existingTeacher = await _context.Teacher.Where(s=> s.TeacherId == teacherId).Include(s=> s.Courses).FirstOrDefaultAsync();
            var existingTeacher = await _context.Teacher.Where(s=> s.TeacherId == teacherId)
                .Include(s=> s.Courses)
                .Select(s=> new
                {
                    naTeacherName = s.TeacherName, s.TeacherId, s.TeacherType, s.Courses
                })
                .FirstOrDefaultAsync();
            if (existingTeacher is null)
            {
                return NotFound();
            }
            return Ok(existingTeacher);
        }

       

        /*public string MyJSONFormatter(Object J)
        {
            string json = JsonConvert.SerializeObject(J, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        
       // 
       [HttpGet][HttpPatch][HttpPost][HttpDelete]
       public  ActionResult<String> NotFoundAction()
       {
            Console.WriteLine("Not Found Yeah");
           return NotFound();
       }*/
    }
    
    
    
    
}