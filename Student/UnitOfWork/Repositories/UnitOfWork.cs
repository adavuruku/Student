using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.UnitOfWork.Service;

namespace Student.UnitOfWork.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly DataContext _context;

        public ICourseRepository Course { get; }
        public ITeacherRepository Teacher { get; }
        
        public UnitOfWork(DataContext context){
            _context =  context;
            Course = new CourseRepository(_context);
            Teacher = new TeacherRepository(_context);
        }


        public async Task<int> Complete(){
            return await _context.SaveChangesAsync();
        }

        public async void Dispose(){
            await _context.DisposeDBContextAsync();
           
        }
    }
}