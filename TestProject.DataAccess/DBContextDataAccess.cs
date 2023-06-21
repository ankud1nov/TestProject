using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Domain.Entities;
using TestProject.Domain.Models.Reports;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public void SaveOrUpdate<T>(T value) where T : class
        {
            switch (value)
            {
                case Company:
                    SaveOrUpdate(value as Company);
                    break;
                case Department:
                    SaveOrUpdate(value as Department);
                    break;
                case Employee:
                    SaveOrUpdate(value as Employee);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public async void Delete<T>(T value) where T : class
        {
            switch (value)
            {
                case Company:
                    DeleteIfNeed(value as Company);
                    break;
                case Department:
                    DeleteIfNeed(value as Department);
                    break;
                case Employee:
                    DeleteIfNeed(value as Employee);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private async void SaveOrUpdate(Company company)
        {
            var exist = _applicationDbContext.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (exist == default)
            {
                await _applicationDbContext.AddAsync(company);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                await _applicationDbContext.SaveChangesAsync();
            }
        }

        private async void SaveOrUpdate(Department department)
        {
            var exist = _applicationDbContext.Departments.FirstOrDefault(x => x.Id == department.Id);
            if (exist == default)
            {
                await _applicationDbContext.AddAsync(department);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                await _applicationDbContext.SaveChangesAsync();
            }

        }

        private async void SaveOrUpdate(Employee employee)
        {
            var exist = _applicationDbContext.Employees.FirstOrDefault(x => x.Id == employee.Id);
            if (exist == default)
            {
                await _applicationDbContext.AddAsync(employee);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                await _applicationDbContext.SaveChangesAsync();
            }
        }

        private async void DeleteIfNeed(Company company)
        {
            if (_applicationDbContext.Companies.Any(x => x.Id == company.Id))
            {
                _applicationDbContext.Remove(company);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
        private async void DeleteIfNeed(Department department)
        {
            if (_applicationDbContext.Companies.Any(x => x.Id == department.Id))
            {
                _applicationDbContext.Remove(department);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
        private async void DeleteIfNeed(Employee employee)
        {
            if (_applicationDbContext.Companies.Any(x => x.Id == employee.Id))
            {
                _applicationDbContext.Remove(employee);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
