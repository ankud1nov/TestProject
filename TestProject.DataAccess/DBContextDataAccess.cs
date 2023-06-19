using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TestProject.Domain.Entities;
using TestProject.Domain.Models.Reports;

namespace TestProject.DataAccess
{
    public class DBContextDataAccess
    {
        private readonly IConfiguration _configuration;
        private Company[] _companies;

        public DBContextDataAccess(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }

        public ICollection<Company> GetAllCompanyes()
        {
            if (_companies != null) return _companies;

            var company = _configuration
                .GetRequiredSection("Company")
                .Get<Company>();

            var departments = _configuration
                .GetRequiredSection("Departments")
                .Get<Department[]>();

            var employees = _configuration
                .GetRequiredSection("Employees")
                .Get<Employee[]>();

            foreach (var employee in employees)
            {
                employee.Department = departments.FirstOrDefault(x => x.Id == employee.DepartamentId);
            }

            foreach (var department in departments)
            {
                department.DepartmentHead = employees.FirstOrDefault(x => x.DepartamentId == department.Id && x.Position == "Начальник отдела");
                department.DepartmentHeadId = department.DepartmentHead?.Id ?? department.DepartmentHeadId;
                department.Company = company;
                department.Employees = employees.Where(x => x.DepartamentId == department.Id).ToArray();
            }

            company.Departments = departments;
            _companies = new Company[] { company };

            return _companies;
        }

        public ICollection<EmployeeSalaryModel> GetEmployeesSalary()
        {
            if (_companies == null) GetAllCompanyes();

            return _companies
                .SelectMany(x => x.Departments)
                .SelectMany(x => x.Employees)
                .Select(x => new EmployeeSalaryModel
                {
                    Company = x.Department.Company.Name,
                    Department = x.Department.Name,
                    EmployeeSalary = x.Salary,
                    Employee = x.FullName
                }).ToArray();
        }
    }
}
