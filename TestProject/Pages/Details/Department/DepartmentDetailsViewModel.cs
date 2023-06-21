using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;

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

        public DepartmentDetailsViewModel(Department department) : base(department)
        {
            foreach (var employee in department.Employees)
            {
                Employees.Add(new EmployeeDetailsViewModel(employee));
            }

            DepartmentHead = department.DepartmentHead.EmployeeHead;
        }
    }
}
