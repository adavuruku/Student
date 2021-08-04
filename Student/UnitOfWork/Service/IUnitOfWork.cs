using System;
using System.Threading.Tasks;

namespace Student.UnitOfWork.Service
{
    //https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
    public interface IUnitOfWork:IDisposable
    {
        ICourseRepository Course {get;}
        ITeacherRepository Teacher {get;}
        Task<int> Complete();
    }
}