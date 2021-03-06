﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace SELPR.ViewModels
{
    public class ProcessDescriptorViewModel: BindableBase
    {
        private DateTime _timeCreated;
        public DateTime TimeCreated
        {
            get { return _timeCreated; }
            set { SetProperty(ref _timeCreated, value); }
        }

        private int _processId;
        public int ProcessId
        {
            get { return _processId; }
            set { SetProperty(ref _processId, value); }
        }

        private string _processName;
        public string ProcessName {
            get { return _processName; }
            set { SetProperty(ref _processName, value); }
        }

        private ProcessDescriptorViewModel _parent;
        public ProcessDescriptorViewModel Parent
        {
            get { return _parent; }
            set
            {
                SetProperty(ref _parent, value);
                OnPropertyChanged(nameof(HasParent));
            }
        }

        public bool HasParent => Parent != null;

        private string _commandLine;
        public string CommandLine
        {
            get { return _commandLine; }
            set { SetProperty(ref _commandLine, value); }
        }

        private ObservableCollection<ProcessDescriptorViewModel> _childrenProcesses;
        public ObservableCollection<ProcessDescriptorViewModel> ChildrenProcessess
        {
            get { return _childrenProcesses; }
            set { SetProperty(ref _childrenProcesses, value); }
        }

        public ProcessDescriptorViewModel() { }

        public ProcessDescriptorViewModel(ProcessDescriptor process): this(process, null) { }

        private ProcessDescriptorViewModel(ProcessDescriptor process, ProcessDescriptorViewModel parent)
        {
            TimeCreated = process.TimeCreated;
            ProcessId = process.ProcessId;
            ProcessName = process.ProcessName;
            CommandLine = process.CommandLine;

            if (process.ChildrenProcesses?.Any() == true)
                ChildrenProcessess = new ObservableCollection<ProcessDescriptorViewModel>(process.ChildrenProcesses.Select(cp => new ProcessDescriptorViewModel(cp, this)));

            // we've been invoked by a parent creating it's children, do not fall into recursion
            if (parent != null)
            {
                Parent = parent;
            }
            // we weren't invoked by a parent creating it's children, but we still have a parent to map
            else if(process.Parent != null)
            {
                Parent = new ProcessDescriptorViewModel(process.Parent);
            }
        }
    }
}
