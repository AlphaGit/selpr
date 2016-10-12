using System;
using System.Linq;
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
        private readonly IBrowseFileCommand _browseFileCommand;
        private readonly IEventLogFileService _eventLogFileService;

        public MainWindowViewModel(IBrowseFileCommand browseFileCommand, IEventLogFileService eventLogFileService)
        {
            if (browseFileCommand == null) throw new ArgumentNullException(nameof(browseFileCommand));
            if (eventLogFileService == null) throw new ArgumentNullException(nameof(eventLogFileService));

            _browseFileCommand = browseFileCommand;
            _eventLogFileService = eventLogFileService;

            BrowseFileCommand = new DelegateCommand(BrowseAndOpenFile, () => true);
            DropOnWindowCommand = new DelegateCommand<IDataObject>(OnDrop, d => true);
            GiveFeedbackCommand = new DelegateCommand<GiveFeedbackEventArgs>(OnGiveFeedback, g => true);
        }

        #region Bindable properties
        private bool _isProcessCanvasVisible = false;
        public bool IsProcessCanvasVisible
        {
            get { return _isProcessCanvasVisible; }
            set { SetProperty(ref _isProcessCanvasVisible, value); }
        }

        private bool _isBrowseButtonVisible = true;
        public bool IsBrowseButtonVisible
        {
            get { return _isBrowseButtonVisible; }
            set { SetProperty(ref _isBrowseButtonVisible, value); }
        }
        #endregion

        #region Commands
        public DelegateCommand BrowseFileCommand { get; set; }
        public DelegateCommand<IDataObject> DropOnWindowCommand { get; set; }
        public DelegateCommand<GiveFeedbackEventArgs> GiveFeedbackCommand { get; set; }
        #endregion

        #region Sub view-models

        private ProcessCanvasViewModel _processCanvas;

        public ProcessCanvasViewModel ProcessCanvas
        {
            get { return _processCanvas; }
            set { SetProperty(ref _processCanvas, value); }
        }

        #endregion

        private void BrowseAndOpenFile()
        {
            var fileName = _browseFileCommand.Execute();
            if (!string.IsNullOrWhiteSpace(fileName))
                OpenFile(fileName);
        }

        private void OpenFile(string fileName)
        {
            var processes = _eventLogFileService.OpenFile(fileName);
            var processesViewModels = processes?.Select(p => new ProcessDescriptorViewModel(p)).ToList();
            ProcessCanvas = new ProcessCanvasViewModel(processesViewModels);
            IsProcessCanvasVisible = true;
            IsBrowseButtonVisible = false;
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
