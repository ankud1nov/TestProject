using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;
using System.Windows.Input;
using TestProject.Domain.Extensions;

namespace TestProject.Pages
{
    public partial class DepartmentDetailsViewModel : DetailsViewModel<Department>
    {
        private Employee _employee;
        public Employee DepartmentHead
        {
            get => _employee;
            set
            {
                if (SetProperty(ref _employee, value, nameof(DepartmentHead)))
                {
                    if (Value.DepartmentHead != null)
                    {
                        Value.DepartmentHead.EmployeeHead = _employee;
                    }
                    else
                    {
                        Value.DepartmentHead = new DepartmentHead
                        {
                            EmployeeHead = _employee,
                            Department = Value
                        };
                    }
                    Value.DepartmentHead.EmployeeHead = _employee;
                }
            }
        }

        private ObservableCollection<EmployeeDetailsViewModel> _employees = new ObservableCollection<EmployeeDetailsViewModel>();
        public ObservableCollection<EmployeeDetailsViewModel> Employees
        {
            get => _employees;
            set => SetProperty(ref _employees, value, nameof(Employees));
        }

        [RelayCommand]
        private void ShowDetails()
        {
            StrongReferenceMessenger.Default.Send(new ValueChangedMessage<DepartmentDetailsViewModel>(this));
        }

        public ICommand AddEmployeeCommand { get; }

        public DepartmentDetailsViewModel(Department department) : base(department)
        {
            foreach (var employee in department.Employees.EmptyIfNull())
            {
                Employees.Add(new EmployeeDetailsViewModel(employee, Value.Company.Departments));
            }

            DepartmentHead = department?.DepartmentHead?.EmployeeHead;

            AddEmployeeCommand = new RelayCommand(AddEmployee);
        }

        private void AddEmployee()
        {
            var newEmployee = new EmployeeDetailsViewModel(new Employee(), Value.Company.Departments);
            var mainVM = ServicesLocator.Current.GetRequiredService<MainWindowViewModel>();
            mainVM.uiDispatcher.Invoke(() => Employees.Add(newEmployee));
            mainVM.uiDispatcher.BeginInvoke(() => mainVM.PaginatorInstance.SetDeatilsPage(newEmployee));
        }
    }
}
