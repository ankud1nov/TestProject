using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TestProject.DataAccess;
using TestProject.Messages;
using TestProject.Pages;
using TestProject.Reports;

namespace TestProject
{
    internal class MainWindowViewModel : 
        ObservableObject,
        IRecipient<ValueChangedMessage<CompanyDetailsViewModel>>,
        IRecipient<ValueChangedMessage<DepartmentDetailsViewModel>>,
        IRecipient<ValueChangedMessage<EmployeeDetailsViewModel>>,
        IRecipient<NeedRefreshMessage>,
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

        public ICommand RefreshCommand { get; }
        public ICommand AddCompanyCommand { get; }

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
            StrongReferenceMessenger.Default.Register<NeedRefreshMessage>(this);

            RefreshCommand = new RelayCommand(() => Task.Run(() => ClearAndRefresh()));
            AddCompanyCommand = new RelayCommand(AddCompany);
            Task.Run(() => FillDefaultCompanies());
        }

        private void ClearAndRefresh()
        {
            _uiDispatcher.Invoke(() => _companies.Clear());

            FillDefaultCompanies();
        }

        private void FillDefaultCompanies()
        {
            var companyes = _dBContextDataAccess.GetAllCompanyes();
            
            foreach (var item in companyes)
                _uiDispatcher.Invoke(() => _companies.Add(new CompanyDetailsViewModel(item)));

            if (_companies.Any())
                _uiDispatcher.BeginInvoke(() => _paginator.SetDeatilsPage(_companies.FirstOrDefault()));
        }

        private void AddCompany()
        {
            var newCompany = new CompanyDetailsViewModel(new Domain.Entities.Company());
            _uiDispatcher.Invoke(() => _companies.Add(newCompany));
            _uiDispatcher.BeginInvoke(() => _paginator.SetDeatilsPage(newCompany));
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

        public void Receive(NeedRefreshMessage message)
        {
            ClearAndRefresh();
        }
    }
}
