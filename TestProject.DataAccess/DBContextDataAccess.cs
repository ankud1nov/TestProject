using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TestProject.Domain.Entities;

namespace TestProject.DataAccess
{
    public class DBContextDataAccess
    {
        private readonly IConfiguration _configuration;

        public DBContextDataAccess(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public ICollection<Company> GetAllCompanyes()
        {
            var company = _configuration
                .GetRequiredSection("Company")
                .Get<Company>();

            var departments = _configuration
                .GetRequiredSection("Departments")
                .Get<Department[]>();

            var employees = _configuration
                .GetRequiredSection("Employees")
                .Get<Employee[]>();

            foreach (var department in departments)
            {
                department.DepartmentHead = employees.FirstOrDefault(x => x.DepartamentId == department.Id && x.Position == "Начальник отдела");
                department.DepartmentHeadId = department.DepartmentHead?.Id ?? department.DepartmentHeadId;
                department.Company = company;
                department.Employees = employees.Where(x => x.DepartamentId == department.Id).ToArray();
            }

            company.Departments = departments;

            return new Company[] { company };
        }
    }
}
