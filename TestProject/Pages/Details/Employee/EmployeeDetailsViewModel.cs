using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using TestProject.Domain.Entities;
using TestProject.Pages.Details;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace TestProject.Pages
{
    public partial class EmployeeDetailsViewModel : DetailsViewModel<Employee>
    {
        private Department _department;
        public Department CurrentDepartment 
        {
            get => _department;
            set => SetProperty(ref _department, value, nameof(CurrentDepartment));
        }
        public ICollection<Department> Departments { get; set; }

        public EmployeeDetailsViewModel(Employee employee) : base(employee)
        {
            var conf = ServicesLocator.Current.GetRequiredService<IConfiguration>();
            Departments = conf
                    .GetRequiredSection("Departments")
                    .Get<Department[]>();

            CurrentDepartment = Departments.FirstOrDefault(x => x.Id == Value.DepartamentId);
        }

        [RelayCommand]
        private void ShowDetails()
        {
            StrongReferenceMessenger.Default.Send(new ValueChangedMessage<EmployeeDetailsViewModel>(this));
        }
    }
}
