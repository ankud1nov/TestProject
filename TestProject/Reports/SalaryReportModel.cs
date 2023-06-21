using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TestProject.DataAccess;
using TestProject.Domain.Models.Reports;

namespace TestProject.Reports;

internal class SalaryReportViewModel : ObservableObject
{
    private ICollectionView _employeeSalaryCollection;
    public ICollectionView EmployeeSalaryCollection 
    {
        get => _employeeSalaryCollection;
        set
        {
            SetProperty(ref _employeeSalaryCollection, value, nameof(EmployeeSalaryCollection));
        }        
    }

    public ICommand RefreshCommand { get; }
    
    public SalaryReportViewModel(ICollection<EmployeeSalaryModel> employeeSalaryes) 
    {
        RefreshData(employeeSalaryes);

        RefreshCommand = new RelayCommand(Refresh);
    }

    private void Refresh()
    {
        var dBContextDataAccess = ServicesLocator.Current.GetRequiredService<DBContextDataAccess>();
        var slaryData = dBContextDataAccess.GetEmployeesSalary();
        RefreshData(slaryData);
    }

    private void RefreshData(ICollection<EmployeeSalaryModel> employeeSalaryes)
    {
        EmployeeSalaryCollection = CollectionViewSource.GetDefaultView(employeeSalaryes);
        EmployeeSalaryCollection.GroupDescriptions.Clear();
        EmployeeSalaryCollection.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Company)));
        EmployeeSalaryCollection.GroupDescriptions.Add(new PropertyGroupDescription(nameof(EmployeeSalaryModel.Department)));
    }
}
