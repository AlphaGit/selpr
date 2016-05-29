using System.Collections.Generic;
using System.Linq;
using SecurityProcessReader;

namespace SELPR
{
    class ProcessTreeGenerator
    {
        public List<ProcessDescriptor> ParseLogEntriesToProcessTree(List<SecurityEventLogEntry> logEntries)
        {
            // clone list
            var pendingToAnalyzeLogEntries = logEntries.Select(l => l).ToList();

            // identify roots first
            var instancedProcessIds = logEntries.Select(le => le.NewProcessId);
            var logEntriesWithoutParent = pendingToAnalyzeLogEntries
                .Where(le => !instancedProcessIds.Contains(le.ParentProcessId))
                .ToList();

            foreach (var logEntryWithoutParent in logEntriesWithoutParent)
                pendingToAnalyzeLogEntries.Remove(logEntryWithoutParent);

            var rootProcesses = logEntriesWithoutParent.Select(ConvertLogEntryToProcessDescriptor).ToList();

            foreach (var root in rootProcesses)
                GetChildrenProcessDescriptors(root, ref pendingToAnalyzeLogEntries);

            return rootProcesses;
        }

        private void GetChildrenProcessDescriptors(ProcessDescriptor parent, ref List<SecurityEventLogEntry> pendingToAnalyzeLogEntries)
        {
            var children = pendingToAnalyzeLogEntries.Where(p => p.ParentProcessId == parent.ProcessId).ToList();
            foreach (var c in children)
            {
                pendingToAnalyzeLogEntries.Remove(c);

                var childrenProcess = ConvertLogEntryToProcessDescriptor(c);
                parent.ChildrenProcesses.Add(childrenProcess);
                childrenProcess.Parent = parent;
            }

            foreach (var c in parent.ChildrenProcesses)
                GetChildrenProcessDescriptors(c, ref pendingToAnalyzeLogEntries);
        }

        private ProcessDescriptor ConvertLogEntryToProcessDescriptor(SecurityEventLogEntry entry)
        {
            return new ProcessDescriptor()
            {
                TimeCreated = entry.TimeCreated,
                CommandLine = entry.CommandLine,
                ProcessId = entry.NewProcessId,
                ProcessName = entry.NewProcessName
            };
        }
    }
}
