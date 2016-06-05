using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SELPR.Commands;
using SELPR.Services;

namespace SELPR.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        public bool IsProcessCanvasVisible => _isEventFileLoaded;
        public BrowseFileCommand BrowseFileCommand { get; set; }
        public bool IsBrowseButtonVisible => BrowseFileCommand.CanExecute(null) && !_isEventFileLoaded;
        public DelegateCommand<IDataObject> DropOnWindowCommand { get; set; }
        public DelegateCommand<GiveFeedbackEventArgs> GiveFeedbackCommand { get; set; }

        public MainWindowViewModel()
        {
            BrowseFileCommand = new BrowseFileCommand(); //TODO should call OpenFile after this, but then, how to isolate its logic?
            DropOnWindowCommand = new DelegateCommand<IDataObject>(OnDrop, (d) => true);
            GiveFeedbackCommand = new DelegateCommand<GiveFeedbackEventArgs>(OnGiveFeedback, (g) => true);
        }

        private bool _isEventFileLoaded;

        private readonly EventLogFileService _eventLogFileService = new EventLogFileService();

        private void OpenFile(string fileName)
        {
            _eventLogFileService.OpenFile(fileName);
            _isEventFileLoaded = true;
        }

        private void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEventArgs)
        {
            Mouse.SetCursor(Cursors.Pen);
        }

        private void OnDrop(IDataObject data)
        {
            if (!data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] fileNames = (string[])data.GetData(DataFormats.FileDrop);
            OpenFile(fileNames[0]);
        }
    }
}
