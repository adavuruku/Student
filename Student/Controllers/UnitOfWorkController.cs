using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Student.Data;
using Student.Models;
using Student.UnitOfWork.Service;

namespace Student.Controllers
{
   
    [Route("api/unit")]
    public class UnitOfWorkController:ControllerBase
    {
        private readonly UnitOfWork.Repositories.UnitOfWork _iuUnitOfWork;
        private readonly DataContext _dataContext;
        
        public UnitOfWorkController(DataContext dataContext)
        {
            _dataContext = dataContext;
            _iuUnitOfWork =  new UnitOfWork.Repositories.UnitOfWork(_dataContext);
        }
        
        
        [HttpPost]
        [Route("new/course")]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            Course cour = new()
            {
                CourseName = course.CourseName,
                Location = course.Location
            };
            _iuUnitOfWork.Course.Add(cour);
            
            Teacher teach = new()
            {
                TeacherName = "Anayo Koro Ohunene"
            };
            _iuUnitOfWork.Teacher.Add(teach);
            
            await _iuUnitOfWork.Complete();
            
            // register the instance so that it is disposed when request ends
            HttpContext.Response.RegisterForDispose(_iuUnitOfWork);
            
            return Ok(new ArrayList(){teach, cour});
            //return CreatedAtAction(nameof(GetAllStudent), student);
        }
        
        [HttpGet]
        [Route("all/course")]
        public async Task<ActionResult<Standard>> GetAllCourse()
        {
            var allCourse  =  _iuUnitOfWork.Course.GetAll();
            return Ok(allCourse);
        }

    }
}