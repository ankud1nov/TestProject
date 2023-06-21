using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Windows;

namespace TestProject.Pages
{
    public partial class EmployeeDetailsViewModel : DetailsViewModel<Employee>
    {
        private Department _department;
        public Department CurrentDepartment 
        {
            get => _department;
            set
            {
                if (SetProperty(ref _department, value, nameof(CurrentDepartment)))
                {
                    Value.DepartmentId = _department.Id;
                }                    
            }
        }

        public bool CanChangeDepartment { get; set; }

        public ICollection<Department> Departments { get; set; }

        public EmployeeDetailsViewModel(Employee employee, ICollection<Department> departments) : base(employee)
        {
            Departments = departments;
            CurrentDepartment = employee.Department;
            CanChangeDepartment = CurrentDepartment?.DepartmentHead?.EmployeeHead.Id != Value.Id;
        }

        [RelayCommand]
        private void ShowDetails()
        {
            StrongReferenceMessenger.Default.Send(new ValueChangedMessage<EmployeeDetailsViewModel>(this));
        }
    }
}
