using System.Collections.Generic;

namespace SELPR.Services
{
    public class EventLogFileService
    {
        public List<ProcessDescriptor> OpenFile(string fileName)
        {
            var logEntries = new SecurityEventLogFileParser().OpenEventLogFile(fileName);
            return new ProcessTreeGenerator().ParseLogEntriesToProcessTree(logEntries);
        }
    }
}
