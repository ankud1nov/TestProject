using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Entities;

namespace TestProject.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<DepartmentHead> DepartmentHeads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasIndex(u => u.Name)
            .IsUnique();
        modelBuilder.Entity<Department>()
            .HasIndex(u => u.Name)
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.Departments)
            .WithOne(d => d.Company)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Department>()
            .HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DepartmentHead>()
            .HasOne(d => d.Department);

        modelBuilder.Entity<DepartmentHead>()
            .HasOne(d => d.EmployeeHead);

        base.OnModelCreating(modelBuilder);
    }
}
