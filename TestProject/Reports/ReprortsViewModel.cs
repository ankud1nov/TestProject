using CommunityToolkit.Mvvm.ComponentModel;
using TestProject.DataAccess;
using TestProject.Domain.Models.Reports;

namespace TestProject.Reports
{
    public class ReprortsViewModel : ObservableObject
    {
        private readonly DBContextDataAccess _dBContextDataAccess;
        public string Text { get; set; } = "Привет";

        public SalaryReportModel SalaryReport { get; set; }

        public ReprortsViewModel(DBContextDataAccess dBContextDataAccess)
        {
            _dBContextDataAccess = dBContextDataAccess;

            var data = _dBContextDataAccess.GetEmployeesSalary();
            SalaryReport = new SalaryReportModel(data);
        }
    }
}
