using System.Collections.Generic;

namespace SELPR.Services
{
    public interface IEventLogFileService
    {
        List<ProcessDescriptor> OpenFile(string fileName);
    }
}