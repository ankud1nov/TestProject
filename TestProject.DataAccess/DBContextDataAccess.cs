using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Domain.Entities;
using TestProject.Domain.Models.Reports;

namespace TestProject.DataAccess
{
    public class DBContextDataAccess
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DBContextDataAccess(ApplicationDbContext applicationDbContext) 
        { 
            _applicationDbContext = applicationDbContext;
        }

        public ICollection<Company> GetAllCompanyes()
        {
            return _applicationDbContext.Companies.ToArray();
        }

        public ICollection<EmployeeSalaryModel> GetEmployeesSalary()
        {
            return _applicationDbContext.Companies
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

        public ICollection<EmployeesListItemModel> GetEmployeesForEmployeesListReport()
        {
            var today = DateTime.Today;

            return _applicationDbContext.Companies
                .SelectMany(x => x.Departments)
                .SelectMany(x => x.Employees)
                .Select(x => new EmployeesListItemModel
                {
                    Company = x.Department.Company.Name,
                    Department = x.Department.Name,
                    Employee = x.FullName,
                    EmployeeAge = x.Birthdate.Day >= today.Day 
                        ? today.Year - x.Birthdate.Year
                        : (today.Year - x.Birthdate.Year) -1,
                    EmployeeExperience = today - x.EmploymentDate,
                    BirthdateYear = x.Birthdate.Year
                }).ToArray();
        }
    }
}
