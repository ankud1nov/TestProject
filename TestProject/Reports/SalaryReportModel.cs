using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using TestProject.Domain.Models.Reports;

namespace TestProject.Reports;

internal class SalaryReportViewModel : ObservableObject
{
    public ICollectionView EmployeeSalaryCollection { get; set; }
    
    public SalaryReportViewModel(ICollection<EmployeeSalaryModel> employeeSalaryes) 
    {
        EmployeeSalaryCollection = CollectionViewSource.GetDefaultView(employeeSalaryes);
        EmployeeSalaryCollection.GroupDescriptions.Clear();
        EmployeeSalaryCollection.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Company)));
        EmployeeSalaryCollection.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Department)));
    }
}
