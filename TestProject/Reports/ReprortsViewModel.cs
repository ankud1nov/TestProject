using CommunityToolkit.Mvvm.ComponentModel;
using TestProject.DataAccess;
using TestProject.Domain.Models.Reports;

namespace TestProject.Reports
{
    internal class ReprortsViewModel : ObservableObject
    {
        private readonly DBContextDataAccess _dBContextDataAccess;

        public SalaryReportViewModel SalaryReport { get; set; }
        public EmployeesListViewModel EmployeesList { get; set; }

        public ReprortsViewModel(DBContextDataAccess dBContextDataAccess)
        {
            _dBContextDataAccess = dBContextDataAccess;

            var slaryData = _dBContextDataAccess.GetEmployeesSalary();
            SalaryReport = new SalaryReportViewModel(slaryData);

            var employeesData = _dBContextDataAccess.GetEmployeesForEmployeesListReport();
            EmployeesList = new EmployeesListViewModel(employeesData);
        }
    }
}
