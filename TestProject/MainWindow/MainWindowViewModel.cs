using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;
using System.Linq;
using TestProject.Domain.Entities;

namespace TestProject
{
    public class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<Company> _companies = new ObservableCollection<Company>();
        public ObservableCollection<Company> Companies => _companies;  
        public MainWindowViewModel(IConfiguration configuration) 
        {
            FillDefaultCompanies(configuration);
        }

        public void FillDefaultCompanies(IConfiguration configuration)
        {
            var company = configuration
                    .GetRequiredSection("Company")
                    .Get<Company>();

            var departments = configuration
                    .GetRequiredSection("Departments")
                    .Get<Department[]>();

            var employees = configuration
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

            _companies.Add(company);
        }
    }
}
