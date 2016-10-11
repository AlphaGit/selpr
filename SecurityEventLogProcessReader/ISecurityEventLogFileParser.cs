using System.Collections.Generic;

namespace SELPR
{
    public interface ISecurityEventLogFileParser
    {
        List<SecurityEventLogEntry> OpenEventLogFile(string fileName);
    }
}