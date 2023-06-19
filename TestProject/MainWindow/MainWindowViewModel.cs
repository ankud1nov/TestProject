using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestProject.DataAccess;
using TestProject.Pages;
using TestProject.Reports;

namespace TestProject
{
    public class MainWindowViewModel : 
        ObservableObject,
        IRecipient<ValueChangedMessage<CompanyDetailsViewModel>>,
        IRecipient<ValueChangedMessage<DepartmentDetailsViewModel>>,
        IRecipient<ValueChangedMessage<EmployeeDetailsViewModel>>,
        IDisposable
    {
        private readonly DBContextDataAccess _dBContextDataAccess;
        private readonly Dispatcher _uiDispatcher;
        private readonly Paginator _paginator;

        private ObservableCollection<CompanyDetailsViewModel> _companies = new ObservableCollection<CompanyDetailsViewModel>();

        public Paginator PaginatorInstance => _paginator;
        public Dispatcher uiDispatcher => _uiDispatcher;
        public ObservableCollection<CompanyDetailsViewModel> Companies => _companies;
        public ReprortsViewModel ReprortsViewModel { get; set; }

        public MainWindowViewModel(
            ReprortsViewModel reprortsViewModel,
            DBContextDataAccess dBContextDataAccess,
            Paginator paginator) 
        {
            _uiDispatcher = Dispatcher.CurrentDispatcher;
            _dBContextDataAccess = dBContextDataAccess;
            _paginator = paginator;
            ReprortsViewModel = reprortsViewModel;

            StrongReferenceMessenger.Default.Register<ValueChangedMessage<CompanyDetailsViewModel>>(this);
            StrongReferenceMessenger.Default.Register<ValueChangedMessage<DepartmentDetailsViewModel>>(this);
            StrongReferenceMessenger.Default.Register<ValueChangedMessage<EmployeeDetailsViewModel>>(this);
            
            Task.Run(() => FillDefaultCompanies());
        }

        public void FillDefaultCompanies()
        {
            var companyes = _dBContextDataAccess.GetAllCompanyes();

            
            foreach (var item in companyes)
                _uiDispatcher.Invoke(() => _companies.Add(new CompanyDetailsViewModel(item)));

            if (_companies.Any())
                _uiDispatcher.BeginInvoke(() => _paginator.SetDeatilsPage(_companies.FirstOrDefault()));
        }

        public void Receive(ValueChangedMessage<CompanyDetailsViewModel> message)
        {
            _paginator.SetDeatilsPage(message.Value);
        }

        public void Receive(ValueChangedMessage<DepartmentDetailsViewModel> message)
        {
            _paginator.SetDeatilsPage(message.Value);
        }

        public void Receive(ValueChangedMessage<EmployeeDetailsViewModel> message)
        {
            _paginator.SetDeatilsPage(message.Value);
        }

        public void Dispose()
        {
            StrongReferenceMessenger.Default.UnregisterAll(this);
        }
    }
}
