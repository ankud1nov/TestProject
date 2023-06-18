using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace TestProject
{
    public class ServicesLocator
    {
        private readonly IHost _host;
        private static ServicesLocator _servicesLocator;

        public static ServicesLocator Current => 
            _servicesLocator ??= new ServicesLocator();

        private ServicesLocator()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                }).Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            RegisterPages(services);
            RegisterViewModels(services);
            RegisterContext(services);
        }

        private void RegisterContext(IServiceCollection services)
        {
        }

        private void RegisterPages(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
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