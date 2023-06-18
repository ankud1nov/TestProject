using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

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
            RegisterViews(services);
            RegisterViewModels(services);
            RegisterContext(services);
        }

        private void RegisterContext(IServiceCollection services)
        {
        }

        private void RegisterViews(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>((s) => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
        }

        private void RegisterViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
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