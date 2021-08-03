using System.Collections.Generic;
using Student.Models;

namespace Student.UnitOfWork.Service
{
    public interface ITeacherRepository:IRepository<Teacher>
    {
        IEnumerable<Teacher> getTopTeachersAndCourses(int pageIndex, int pageSize);
    }
}