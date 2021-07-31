using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public class DataContext:DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Models.Student>()
                .HasOne<Grade>(s => s.)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.CurrentGradeId);*/
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Standard> Standard { get; set; }
        public DbSet<Models.Student> Student { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Grade> Grade { get; set; }

    }
}