using System.Collections.Generic;

namespace SELPR
{
    public interface IProcessTreeGenerator
    {
        List<ProcessDescriptor> ParseLogEntriesToProcessTree(List<SecurityEventLogEntry> logEntries);
    }
}