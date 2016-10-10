using Microsoft.Practices.Unity;
using SELPR.Commands;
using SELPR.UiAbstractions;
using SELPR.UiImplementations;
using SELPR.ViewModels;

namespace SELPR
{
    public class DependencyInjector
    {
        public static IUnityContainer Container { get; set; } = new UnityContainer();

        public static void ConfigureDependencies()
        {
            Container.RegisterType<IOpenFileDialog, Win32OpenFileDialog>();

            Container.RegisterType<IBrowseFileCommand, BrowseFileCommand>(new InjectionConstructor(new ResolvedParameter<IOpenFileDialog>()));
            Container.RegisterType<MainWindowViewModel>(new InjectionConstructor(new ResolvedParameter<IBrowseFileCommand>()));
        }
    }
}
