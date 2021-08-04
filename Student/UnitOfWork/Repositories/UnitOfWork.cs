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

        public ICourseRepository _course { get; set; }
        public ITeacherRepository _teacher { get; set; }
        
        public UnitOfWork(DataContext context){
            _context =  context;
            //initialize the repositories in constructor will cause alot of 
            //work anytime a unitOfWork is declared. since all repository is been initialize
            // so is better to use the current getters and setters we are using
            //so that the repositories are only created anytime is needed
            
          //  Course = new CourseRepository(_context);
           // Teacher = new TeacherRepository(_context);
        }
        
        public ICourseRepository Course
        {
            get
            {
                this._course ??= new CourseRepository(_context);
                return _course;
            }
        }
        
        public ITeacherRepository Teacher
        {
            get
            {
                this._teacher ??= new TeacherRepository(_context);
                return _teacher;
            }
        }
        
        public async Task<int> Complete(){
            return await _context.SaveChangesAsync();
        }
        
        
        private bool disposed = false;
        protected virtual async void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeDBContextAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}