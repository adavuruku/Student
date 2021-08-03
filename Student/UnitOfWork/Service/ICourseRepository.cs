using System.Collections.Generic;
using Student.Models;

namespace Student.UnitOfWork.Service
{
    public interface ICourseRepository:IRepository<Course>
    {
        IEnumerable<Course> GetTopSellingCourses(int count);
        IEnumerable<Course> GetCoursesWithStudents(int pageIndex, int pageSize);
    }
}