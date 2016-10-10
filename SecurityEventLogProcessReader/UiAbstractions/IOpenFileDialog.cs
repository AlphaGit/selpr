namespace SELPR.UiAbstractions
{
    public interface IOpenFileDialog
    {
        bool Multiselect { get; set; }
        string Filter { get; set; }
        bool? ShowDialog();
        string FileName { get; set; }
    }
}
