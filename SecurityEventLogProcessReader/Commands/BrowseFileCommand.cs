using System;
using System.Windows.Input;
using Microsoft.Win32;

namespace SELPR.Commands
{
    public class BrowseFileCommand: ICommand
    {
        public string ExecutionResult { get; private set; }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Event log files (*.evtx)|*.evtx"
            };

            ExecutionResult = openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }

        public event EventHandler CanExecuteChanged;
    }
}
