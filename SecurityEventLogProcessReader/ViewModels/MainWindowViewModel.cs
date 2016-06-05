using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SELPR.Commands;

namespace SELPR.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        public bool IsProcessCanvasVisible => _isEventFileLoaded;
        public BrowseFileCommand BrowseFileCommand { get; set; }
        public bool IsBrowseButtonVisible => BrowseFileCommand.CanExecute(null) && !_isEventFileLoaded;
        public OpenEventLogFileCommand OpenEventFileCommand { get; set; } 
        public DelegateCommand<IDataObject> DropOnWindowCommand { get; set; }
        public DelegateCommand<GiveFeedbackEventArgs> GiveFeedbackCommand { get; set; }

        public MainWindowViewModel()
        {
            BrowseFileCommand = new BrowseFileCommand();
            OpenEventFileCommand = new OpenEventLogFileCommand();
            DropOnWindowCommand = new DelegateCommand<IDataObject>(OnDrop, (d) => true);
            GiveFeedbackCommand = new DelegateCommand<GiveFeedbackEventArgs>(OnGiveFeedback, (g) => true);
        }

        private void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
        {
            Mouse.SetCursor(Cursors.Pen);
        }

        private void OnDrop(IDataObject data)
        {
            if (!data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] fileNames = (string[])data.GetData(DataFormats.FileDrop);
            OpenEventFileCommand.Execute(fileNames[0]);
        }

        private bool _isEventFileLoaded;
    }
}
