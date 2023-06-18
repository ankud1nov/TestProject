using System.Windows;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            await ServicesLocator.Current.StartAsync();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await ServicesLocator.Current.StopAsync();
            base.OnExit(e);
        }
    }
}
