﻿using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SELPR.Commands;
using SELPR.Services;

namespace SELPR.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
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
        public ProcessCanvasViewModel ProcessCanvas { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            BrowseFileCommand = new DelegateCommand(BrowseAndOpenFile, () => _browseFileCommand.CanExecute(null));
            DropOnWindowCommand = new DelegateCommand<IDataObject>(OnDrop, d => true);
            GiveFeedbackCommand = new DelegateCommand<GiveFeedbackEventArgs>(OnGiveFeedback, g => true);
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
