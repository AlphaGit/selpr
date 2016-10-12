using System;
using System.Collections.Generic;

namespace SELPR.Services
{
    public class EventLogFileService : IEventLogFileService
    {
        private readonly ISecurityEventLogFileParser _securityEventLogFileParser;
        private readonly IProcessTreeGenerator _processTreeGenerator;

        public EventLogFileService(ISecurityEventLogFileParser securityEventLogFileParser,
            IProcessTreeGenerator processTreeGenerator)
        {
            if (securityEventLogFileParser == null) throw new ArgumentNullException(nameof(securityEventLogFileParser));
            if (processTreeGenerator == null) throw new ArgumentNullException(nameof(processTreeGenerator));

            _securityEventLogFileParser = securityEventLogFileParser;
            _processTreeGenerator = processTreeGenerator;
        }

        public List<ProcessDescriptor> OpenFile(string fileName)
        {
            var logEntries = _securityEventLogFileParser.OpenEventLogFile(fileName);
            return _processTreeGenerator.ParseLogEntriesToProcessTree(logEntries);
        }
    }
}
