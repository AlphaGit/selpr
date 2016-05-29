using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SecurityProcessReader;

namespace SELPR
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false,
                Filter = "Event log files (*.evtx)|*.evtx"
            };

            if (openFileDialog.ShowDialog() != true) return;
            OpenFile(openFileDialog.FileName);
        }

        private void MainWindow_OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);
            Mouse.SetCursor(Cursors.Pen);
        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            base.OnDrop(e);

            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] fileNames = (string[]) e.Data.GetData(DataFormats.FileDrop);
            OpenFile(fileNames[0]);
        }

        private void OpenFile(string fileName)
        {
            var logEntries = new SecurityEventLogFileParser().OpenEventLogFile(fileName);
            var processTree = new ProcessTreeGenerator().ParseLogEntriesToProcessTree(logEntries);
            Debugger.Break();
        }
    }
}
