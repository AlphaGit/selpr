using System.Diagnostics;

namespace SELPR.Services
{
    public class EventLogFileService
    {
        public void OpenFile(string fileName)
        {
            var logEntries = new SecurityEventLogFileParser().OpenEventLogFile(fileName);
            var processTree = new ProcessTreeGenerator().ParseLogEntriesToProcessTree(logEntries);
            Debugger.Break();
        }
    }
}
