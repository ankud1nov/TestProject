using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestProject.Domain.Models.Reports
{
    public class SalaryReportModel
    {
        public ObservableCollection<EmployeeSalaryModel> EmployeeSalary { get; set; }

        public SalaryReportModel(ICollection<EmployeeSalaryModel> employeeSalaries) 
        {
            EmployeeSalary = new ObservableCollection<EmployeeSalaryModel>();
            foreach (var employeeSalary in employeeSalaries)
            {
                EmployeeSalary.Add(employeeSalary);
            }
        }
    }
}
