using Microsoft.Win32;

namespace SELPR.Commands
{
    public class BrowseFileCommand
    {
        public string Execute()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Event log files (*.evtx)|*.evtx"
            };

            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}
