using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public class DataContext:DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }
    }
}