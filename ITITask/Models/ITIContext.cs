using ITITask.Models.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ITITask.Models
{
    public class ITIContext : IdentityDbContext<ApplicationUser>
    {
        public ITIContext()
        {
        }

        public ITIContext(DbContextOptions options)
            :base(options) 
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DeptId);
            modelBuilder.Entity<Employee>().HasQueryFilter(i => i.IsDeleted == false);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Employee)
                .WithMany(e => e.Departments)
                .HasForeignKey(d => d.ManagerId);
            modelBuilder.Entity<Department>().HasQueryFilter(i => i.IsDeleted == false);

        }
    }
}
