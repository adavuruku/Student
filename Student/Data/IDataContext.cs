using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    //the esense of having this here is to expose(inject) the database context
    //it will be serve as a service
    public interface IDataContext 
    {
        DbSet<Product> Products { get; set; }
        DbSet<Standard> Standard { get; set; }
        DbSet<Models.Student> Student { get; set; }
        DbSet<StudentAddress> StudentAddress { get; set; }
        DbSet<Teacher> Teacher { get; set; }
        DbSet<Course> Course { get; set; }
        DbSet<Grade> Grade { get; set; }

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync();
        
        Task DisposeAsync();
    }
}