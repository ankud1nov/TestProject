using System.Threading.Tasks;
using System;
using TestProject.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Linq;
using TestProject.Domain.Entities;
using System.Collections.Generic;

namespace TestProject
{
    public class Initializer
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _applicationDbContext;

        public Initializer(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        public async Task InitialCreate()
        {
            try
            {
                if (!_applicationDbContext.Companies.Any())
                {
                    var company = _configuration
                        .GetRequiredSection("Company")
                        .Get<Company>();
                    await _applicationDbContext.Companies.AddAsync(company);
                    await _applicationDbContext.SaveChangesAsync();

                    var departments = _configuration
                        .GetRequiredSection("Departments")
                        .Get<Department[]>();
                    foreach (var item in departments)
                    {

                        await _applicationDbContext.Departments.AddAsync(item);
                        await _applicationDbContext.SaveChangesAsync();
                    }

                    var employees = _configuration
                        .GetRequiredSection("Employees")
                        .Get<Employee[]>();
                    foreach (var item in employees)
                    {

                        await _applicationDbContext.Employees.AddAsync(item);
                        await _applicationDbContext.SaveChangesAsync();
                    }

                    var depHeads = _configuration
                        .GetRequiredSection("DepartmentHeads")
                        .Get<DepartmentHead[]>();

                    foreach (var item in depHeads)
                    {
                        var depHead = new DepartmentHead
                        {
                            DepartmentId = item.DepartmentId,
                            EmployeeId = item.EmployeeId
                        };
                        await _applicationDbContext.AddAsync(depHead);
                        await _applicationDbContext.SaveChangesAsync();
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(_configuration["ConnectionStrings:DefaultConnection"]);
            }
        }
    }
}
