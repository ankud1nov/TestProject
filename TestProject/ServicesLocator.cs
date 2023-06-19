using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;
using TestProject.DataAccess;
using TestProject.Pages;
using TestProject.Pages.Details.Company;
using TestProject.Pages.Details.Department;
using TestProject.Pages.Details.Employee;
using TestProject.Reports;

namespace TestProject
{
    public class ServicesLocator
    {
        private readonly IHost _host;
        private IConfiguration _configuration;
        private static ServicesLocator _servicesLocator;

        public static ServicesLocator Current => 
            _servicesLocator ??= new ServicesLocator();

        private ServicesLocator()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddSingleton<DBContextDataAccess>();
            services.AddSingleton<Paginator>();

            RegisterViews(services);
            RegisterViewModels(services);
            RegisterContext(services);
        }

        private void RegisterContext(IServiceCollection services)
        {
        }

        private void RegisterViews(IServiceCollection services)
        {
            services.AddTransient<CompanyDetailsPage>();
            services.AddTransient<DepartmentDetailsPage>();
            services.AddTransient<EmployeeDetailsPage>();
            services.AddSingleton<MainWindow>((s) => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
        }

        private void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<CompanyDetailsViewModel>();
            services.AddTransient<DepartmentDetailsViewModel>();
            services.AddTransient<EmployeeDetailsViewModel>();
            services.AddSingleton<ReprortsViewModel>();
        }

        public async Task StartAsync()
        {
            await _host.StartAsync();
            
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public async Task StopAsync()
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        public T GetRequiredService<T>()
        {
            return _host.Services.GetRequiredService<T>();
        }
    }
}