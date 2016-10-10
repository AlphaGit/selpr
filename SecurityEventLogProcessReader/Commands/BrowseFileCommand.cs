using System;
using SELPR.UiAbstractions;

namespace SELPR.Commands
{
    public class BrowseFileCommand : IBrowseFileCommand
    {
        private readonly IOpenFileDialog _openFileDialog;

        public BrowseFileCommand(IOpenFileDialog openFileDialog)
        {
            if (openFileDialog == null) throw new ArgumentNullException(nameof(openFileDialog));

            _openFileDialog = openFileDialog;
        }

        public string Execute()
        {
            _openFileDialog.Multiselect = false;
            _openFileDialog.Filter = "Event log files (*.evtx)|*.evtx";

            return _openFileDialog.ShowDialog() == true ? _openFileDialog.FileName : null;
        }
    }
}
