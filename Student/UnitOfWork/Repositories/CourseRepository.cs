using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using Student.UnitOfWork.Service;

namespace Student.UnitOfWork.Repositories
{
    public class CourseRepository:Repository<Course>, ICourseRepository{
        //base(context) -> initialise / pass the constructor here to the constructor in Repository (inheritance)
        //accessing parameterize constructor
       // private DataContext _objectcontext;
       //this constructor will pass the context to the base constructor
       //since this has inherit from the base base -> the _context is also available here
        public CourseRepository(DataContext context):base(context)
        {
            //_objectcontext = context;
        }

        public IEnumerable<Course> GetTopSellingCourses(int count){
            return _context.Course.OrderByDescending(c=> c.CourseName).Take(count).ToList();
        }

        public IEnumerable<Course> GetCoursesWithStudents(int pageIndex, int pageSize){
            return _context.Course.Include(c=> c.Students)
                .OrderBy(c=> c.CourseId)
                .Skip(pageIndex - 1).Take(pageSize).ToList();
        }
        
    }
    
}