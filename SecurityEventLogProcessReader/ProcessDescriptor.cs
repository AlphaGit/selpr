using System;
using System.Collections.Generic;

namespace SELPR
{
    class ProcessDescriptor
    {
        public DateTime TimeCreated { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public ProcessDescriptor Parent { get; set; }
        public string CommandLine { get; set; }
        public List<ProcessDescriptor> ChildrenProcesses { get; set; } = new List<ProcessDescriptor>();
    }
}
