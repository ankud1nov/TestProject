using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;

namespace TestProject.Pages
{
    public partial class CompanyDetailsViewModel : DetailsViewModel<Company>
    {
        private  ObservableCollection<DepartmentDetailsViewModel> _departments = new ObservableCollection<DepartmentDetailsViewModel>();
        public ObservableCollection<DepartmentDetailsViewModel> Departments
        {
            get => _departments; 
            set => SetProperty(ref _departments, value, nameof(Departments));
        }

        [RelayCommand]
        private void ShowDetails()
        {
            StrongReferenceMessenger.Default.Send(new ValueChangedMessage<CompanyDetailsViewModel>(this));
        }

        public CompanyDetailsViewModel(Company company) : base(company)
        {
            foreach (var department in company.Departments)
            {
                Departments.Add(new DepartmentDetailsViewModel(department));
            }
        }
    }
}
