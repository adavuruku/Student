using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Standard> Standard { get; set; }
        DbSet<Models.Student> Student { get; set; }
        DbSet<StudentAddress> StudentAddress { get; set; }
        DbSet<Teacher> Teacher { get; set; }
        DbSet<Course> Course { get; set; }
        DbSet<Grade> Grade { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}