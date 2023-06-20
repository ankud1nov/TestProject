
using System;

namespace TestProject.Domain.Models.Reports
{
    public class EmployeesListItemModel
    {
        public string Company { get; set; }
        public string Department { get; set; }
        public string Employee { get; set; }
        public TimeSpan EmployeeExperience { get; set; }
        public int EmployeeAge { get; set; }
        public int BirthdateYear { get; set; }
    }
}
