using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;
using Student.UnitOfWork.Service;

namespace Student.UnitOfWork.Repositories
{
    public class TeacherRepository:Repository<Teacher>, ITeacherRepository
    {
        //private DataContext _objectcontext;
        //this constructor will pass the context to the base constructor
        //since this has inherit from the base base -> the _context is also available here
        public TeacherRepository(DataContext context):base(context)
        {
            //_objectcontext = context;
        }
        
        public IEnumerable<Teacher> getTopTeachersAndCourses(int pageIndex, int pageSize)
        {
            return _context.Teacher.Include(c=> c.Courses).OrderBy(c=>c.TeacherId)
                .Skip(pageIndex - 1).Take(pageSize).ToList();
        }
    }
}