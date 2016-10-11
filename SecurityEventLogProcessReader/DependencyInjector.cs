using Microsoft.Practices.Unity;
using SELPR.Commands;
using SELPR.Services;
using SELPR.UiAbstractions;
using SELPR.UiImplementations;
using SELPR.ViewModels;

namespace SELPR
{
    public static class DependencyInjector
    {
        public static IUnityContainer Container { get; set; } = new UnityContainer();

        public static void ConfigureDependencies()
        {
            Container.RegisterType<IOpenFileDialog, Win32OpenFileDialog>();

            Container.RegisterType<IBrowseFileCommand, BrowseFileCommand>(new InjectionConstructor(new ResolvedParameter<IOpenFileDialog>()));
            Container.RegisterType<MainWindowViewModel>(new InjectionConstructor(new ResolvedParameter<IBrowseFileCommand>(), new ResolvedParameter<IEventLogFileService>()));
            
            //TODO how to force this to be disposed after used?
            Container.RegisterType<IEventLogFileReader, EventLogFileFileReader>();
            Container.RegisterType<ISecurityEventLogFileParser, SecurityEventLogFileParser>(new InjectionConstructor(new ResolvedParameter<IEventLogFileReader>()));

            Container.RegisterType<IProcessTreeGenerator, ProcessTreeGenerator>();
            Container.RegisterType<IEventLogFileService, EventLogFileService>(new InjectionConstructor(new ResolvedParameter<ISecurityEventLogFileParser>(), new ResolvedParameter<IProcessTreeGenerator>()));
        }
    }
}
