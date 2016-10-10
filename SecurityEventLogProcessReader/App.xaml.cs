using System.Windows;
using SELPR;

namespace SecurityProcessReader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DependencyInjector.ConfigureDependencies();
            base.OnStartup(e);
        }
    }
}
