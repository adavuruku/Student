using System;
using System.Threading.Tasks;

namespace Student.UnitOfWork.Service
{
    public interface IUnitOfWork:IDisposable
    {
        ICourseRepository Course {get;}
        ITeacherRepository Teacher {get;}
        Task<int> Complete();
    }
}