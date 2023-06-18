using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TestProject.Domain.Entities;
using TestProject.Pages;
using TestProject.Pages.Details.Company;
using TestProject.Pages.Details.Department;
using TestProject.Pages.Details.Employee;

namespace TestProject
{
    public class MainWindowViewModel : 
        ObservableObject,
        IRecipient<ValueChangedMessage<CompanyDetailsViewModel>>,
        IRecipient<ValueChangedMessage<DepartmentDetailsViewModel>>,
        IRecipient<ValueChangedMessage<EmployeeDetailsViewModel>>,
        IDisposable
    {
        private Page _openedPage;
        private Uri _openedUri;
        private ObservableCollection<CompanyDetailsViewModel> _companies = new ObservableCollection<CompanyDetailsViewModel>();

        public Page OpenedPage
        {
            get => _openedPage;
            set => SetProperty(ref _openedPage, value, nameof(OpenedPage));
        }
        public Uri OpenedUri => _openedUri;
        public ObservableCollection<CompanyDetailsViewModel> Companies => _companies;  
        public MainWindowViewModel(IConfiguration configuration) 
        {
            FillDefaultCompanies(configuration);
            StrongReferenceMessenger.Default.Register<ValueChangedMessage<CompanyDetailsViewModel>>(this);
            StrongReferenceMessenger.Default.Register<ValueChangedMessage<DepartmentDetailsViewModel>>(this);
            StrongReferenceMessenger.Default.Register<ValueChangedMessage<EmployeeDetailsViewModel>>(this);
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

            var companyViewModel = new CompanyDetailsViewModel(company);
            _companies.Add(companyViewModel);
            _openedPage = new CompanyDetailsPage(companyViewModel);
        }

        public void Receive(ValueChangedMessage<CompanyDetailsViewModel> message)
        {
            OpenedPage = new CompanyDetailsPage(message.Value);
        }

        public void Receive(ValueChangedMessage<DepartmentDetailsViewModel> message)
        {
            OpenedPage = new DepartmentDetailsPage(message.Value);
        }

        public void Receive(ValueChangedMessage<EmployeeDetailsViewModel> message)
        {
            OpenedPage = new EmployeeDetailsPage(message.Value);
        }

        public void Dispose()
        {
            StrongReferenceMessenger.Default.UnregisterAll(this);
        }
    }
}
