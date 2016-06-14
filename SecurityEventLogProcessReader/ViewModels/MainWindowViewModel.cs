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
        public bool IsProcessCanvasVisible => IsEventFileLoaded;
        public ProcessCanvasViewModel ProcessCanvas;
        public DelegateCommand BrowseFileCommand { get; set; }
        public bool IsBrowseButtonVisible => BrowseFileCommand.CanExecute() && !IsEventFileLoaded;
        public DelegateCommand<IDataObject> DropOnWindowCommand { get; set; }
        public DelegateCommand<GiveFeedbackEventArgs> GiveFeedbackCommand { get; set; }

        public MainWindowViewModel()
        {
            BrowseFileCommand = new DelegateCommand(BrowseAndOpenFile, () => _browseFileCommand.CanExecute(null));
            DropOnWindowCommand = new DelegateCommand<IDataObject>(OnDrop, d => true);
            GiveFeedbackCommand = new DelegateCommand<GiveFeedbackEventArgs>(OnGiveFeedback, g => true);
        }

        private bool _isEventFileLoaded;
        private bool IsEventFileLoaded
        {
            get
            {
                return _isEventFileLoaded;
            }
            set
            {
                SetProperty(ref _isEventFileLoaded, value);
                OnPropertyChanged(nameof(IsEventFileLoaded)); // dependent on this property
                OnPropertyChanged(nameof(IsBrowseButtonVisible)); // dependent on this property
            }
        }

        private readonly EventLogFileService _eventLogFileService = new EventLogFileService();

        private BrowseFileCommand _browseFileCommand = new BrowseFileCommand();

        private void BrowseAndOpenFile()
        {
            _browseFileCommand.Execute(null);
            var fileName = _browseFileCommand.ExecutionResult;
            if (!string.IsNullOrWhiteSpace(fileName))
                OpenFile(fileName);
        }

        private void OpenFile(string fileName)
        {
            var processes = _eventLogFileService.OpenFile(fileName);
            ProcessCanvas = new ProcessCanvasViewModel(processes);
            IsEventFileLoaded = true;
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
