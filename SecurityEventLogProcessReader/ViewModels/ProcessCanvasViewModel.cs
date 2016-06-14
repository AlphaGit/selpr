using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;

namespace SELPR.ViewModels
{
    public class ProcessCanvasViewModel: BindableBase
    {
        public List<ProcessDescriptor> Processes { get; set; }

        public ProcessCanvasViewModel(): this(null)
        { }

        public ProcessCanvasViewModel(List<ProcessDescriptor> processes)
        {
            Processes = processes;
        }
    }
}
