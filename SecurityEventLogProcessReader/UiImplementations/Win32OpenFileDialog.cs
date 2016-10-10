using Microsoft.Win32;
using SELPR.UiAbstractions;

namespace SELPR.UiImplementations
{
    public class Win32OpenFileDialog: IOpenFileDialog
    {
        private readonly OpenFileDialog _openFileDialog;
        public Win32OpenFileDialog()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public bool Multiselect
        {
            get { return _openFileDialog.Multiselect; }
            set { _openFileDialog.Multiselect = value; }
        }

        public string Filter
        {
            get { return _openFileDialog.Filter; }
            set { _openFileDialog.Filter = value; }
        }

        public bool? ShowDialog()
        {
            return _openFileDialog.ShowDialog();
        }

        public string FileName
        {
            get { return _openFileDialog.FileName; }
            set { _openFileDialog.FileName = value; }
        }
    }
}
