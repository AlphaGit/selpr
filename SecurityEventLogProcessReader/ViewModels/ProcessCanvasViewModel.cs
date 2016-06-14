using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;

namespace SELPR.ViewModels
{
    public class ProcessCanvasViewModel: BindableBase
    {
        private List<ProcessDescriptor> _processes;

        public ProcessCanvasViewModel(List<ProcessDescriptor> processes)
        {
            _processes = processes;
        }


    }
}
