using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SELPR.ViewModels
{
    public class ProcessCanvasViewModel: DependencyObject
    {
        public ObservableCollection<ProcessDescriptorViewModel> Processes
        {
            get { return (ObservableCollection<ProcessDescriptorViewModel>) GetValue(ProcessessProperty); }
            set { SetValue(ProcessessProperty, value); }
        }

        public ProcessCanvasViewModel(): this(null)
        { }

        public ProcessCanvasViewModel(List<ProcessDescriptorViewModel> processes)
        {
            if (processes == null)
                processes = new List<ProcessDescriptorViewModel>();

            Processes = new ObservableCollection<ProcessDescriptorViewModel>(processes);
        }

        public static readonly DependencyProperty ProcessessProperty =
            DependencyProperty.Register("Processes", typeof (ObservableCollection<ProcessDescriptorViewModel>), typeof (ProcessCanvasViewModel), new PropertyMetadata(null));
    }
}
