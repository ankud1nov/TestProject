using System.Windows.Controls;

namespace TestProject.Pages.Details.Company
{
    /// <summary>
    /// Логика взаимодействия для CompanyDetailsPage.xaml
    /// </summary>
    public partial class CompanyDetailsPage : Page
    {
        public CompanyDetailsPage(CompanyDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
