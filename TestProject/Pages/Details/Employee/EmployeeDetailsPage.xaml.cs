using System.Windows.Controls;

namespace TestProject.Pages.Details.Employee
{
    /// <summary>
    /// Логика взаимодействия для EmployeeDetailsPage.xaml
    /// </summary>
    public partial class EmployeeDetailsPage : Page
    {
        public EmployeeDetailsPage(EmployeeDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
