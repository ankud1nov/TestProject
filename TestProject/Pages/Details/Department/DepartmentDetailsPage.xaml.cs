using System.Windows.Controls;

namespace TestProject.Pages.Details.Department
{
    /// <summary>
    /// Логика взаимодействия для DepartmentDetailsPage.xaml
    /// </summary>
    public partial class DepartmentDetailsPage : Page
    {
        public DepartmentDetailsPage(DepartmentDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
