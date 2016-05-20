﻿using Microsoft.Practices.Prism.Mvvm;

namespace SecurityProcessReader.ViewModels
{
    public class MainWindowViewModel: BindableBase
    {
        public bool IsBrowseButtonVisible => !_isEventFileLoaded;
        public bool IsProcessCanvasVisible => _isEventFileLoaded;

        private bool _isEventFileLoaded;
    }
}
