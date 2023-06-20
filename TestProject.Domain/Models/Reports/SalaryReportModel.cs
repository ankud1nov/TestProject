using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace TestProject.Domain.Models.Reports
{
    public class SalaryReportModel
    {
        public ObservableCollection<EmployeeSalaryModel> EmployeeSalary { get; set; }
        public ICollectionView Collection { get; set; }
        
        public SalaryReportModel(ICollection<EmployeeSalaryModel> employeeSalaries) 
        {
            EmployeeSalary = new ObservableCollection<EmployeeSalaryModel>();
            foreach (var employeeSalary in employeeSalaries)
            {
                EmployeeSalary.Add(employeeSalary);
            }

            var cvs = CollectionViewSource.GetDefaultView(employeeSalaries);
            cvs.GroupDescriptions.Clear();
            cvs.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Company)));
            cvs.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Department)));

            Collection = cvs;
        }
    }
}
