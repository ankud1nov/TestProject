using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Controls;
using TestProject.Pages.Details.Company;
using TestProject.Pages.Details.Department;
using TestProject.Pages.Details.Employee;

namespace TestProject.Pages
{
    public class Paginator : ObservableObject
    {
        private CompanyDetailsPage CompanyDetailsPage;
        private DepartmentDetailsPage DepartmentDetailsPage;
        private EmployeeDetailsPage EmployeeDetailsPage;
        private Page _openedPage;

        public Page OpenedPage 
        {
            get => _openedPage;
            set => SetProperty(ref _openedPage, value, nameof(OpenedPage));
        }

        public Paginator(
            CompanyDetailsPage companyDetailsPage,
            DepartmentDetailsPage departmentDetailsPage,
            EmployeeDetailsPage employeeDetailsPage)
        {
            CompanyDetailsPage = companyDetailsPage;
            DepartmentDetailsPage = departmentDetailsPage;
            EmployeeDetailsPage = employeeDetailsPage;
        }

        public void SetDeatilsPage<T>(T pageViewModel)
        {
            if (pageViewModel == null)
                return;

            switch (pageViewModel)
            {
                case CompanyDetailsViewModel:
                    CompanyDetailsPage.DataContext = pageViewModel;
                    OpenedPage = CompanyDetailsPage;
                    break;
                case DepartmentDetailsViewModel:
                    DepartmentDetailsPage.DataContext = pageViewModel;
                    OpenedPage = DepartmentDetailsPage;
                    break;
                case EmployeeDetailsViewModel:
                    EmployeeDetailsPage.DataContext = pageViewModel;
                    OpenedPage = EmployeeDetailsPage;
                    break;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
