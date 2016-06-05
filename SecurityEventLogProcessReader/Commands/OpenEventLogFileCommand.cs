using System;
using System.Windows.Input;
using SELPR.Services;

namespace SELPR.Commands
{
    public class OpenEventLogFileCommand: ICommand
    {
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var fileName = parameter as string;
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Filename needs to have a value", nameof(parameter));

            _eventLogFileService.OpenFile(fileName);
        }

        public event EventHandler CanExecuteChanged;

        private EventLogFileService _eventLogFileService = new EventLogFileService();
    }
}
