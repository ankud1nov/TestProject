using System.Collections.ObjectModel;

namespace TestProject.Models
{
    public class SalaryReportModel
    {
        public ObservableCollection<EmployeeSalaryModel> EmployeeSalary { get; set; }
    }
}
