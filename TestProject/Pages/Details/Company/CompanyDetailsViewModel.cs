using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;
using System.Linq;
using TestProject.Domain.Extensions;
using System.Windows.Input;

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

        public ICommand AddDepartmentCommand { get; }

        [RelayCommand]
        private void ShowDetails()
        {
            StrongReferenceMessenger.Default.Send(new ValueChangedMessage<CompanyDetailsViewModel>(this));
        }

        public CompanyDetailsViewModel(Company company) : base(company)
        {
            foreach (var department in company.Departments.EmptyIfNull())
            {
                Departments.Add(new DepartmentDetailsViewModel(department));
            }
            AddDepartmentCommand = new RelayCommand(AddDepartment);
        }

        private void AddDepartment()
        {
            var newDepartment = new DepartmentDetailsViewModel(new Department { CompanyId = Value.Id});
            var mainVM = ServicesLocator.Current.GetRequiredService<MainWindowViewModel>();
            mainVM.uiDispatcher.Invoke(() => Departments.Add(newDepartment));
            mainVM.uiDispatcher.BeginInvoke(() => mainVM.PaginatorInstance.SetDeatilsPage(newDepartment));
        }
    }
}
