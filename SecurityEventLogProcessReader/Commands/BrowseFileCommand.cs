using SELPR.UiAbstractions;

namespace SELPR.Commands
{
    public class BrowseFileCommand : IBrowseFileCommand
    {
        private readonly IOpenFileDialog _openFileDialog;

        public BrowseFileCommand(IOpenFileDialog openFileDialog)
        {
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
