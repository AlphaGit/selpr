using Microsoft.Practices.Unity;
using SELPR.ViewModels;

namespace SELPR
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => DependencyInjector.Container.Resolve<MainWindowViewModel>();
    }
}
