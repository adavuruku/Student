using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Models;

namespace Student.Data
{
    public class DataContext:DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> option) : base(option)
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
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
        
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        //override the saveChanges to add the createdAt and UpdatedAt
        public async Task<int> SaveChangesAsync()
        {
            
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

    }
}