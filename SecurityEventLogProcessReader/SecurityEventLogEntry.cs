using System;

namespace SELPR
{
    class SecurityEventLogEntry
    {
        public DateTime TimeCreated { get; set; }
        public int NewProcessId { get; set; }
        public string NewProcessName { get; set; }
        public int ParentProcessId { get; set; }
        public string CommandLine { get; set; }
    }
}
