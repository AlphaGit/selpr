using System;
using System.Diagnostics.Eventing.Reader;

namespace SELPR
{
    public interface IEventLogFileReader: IDisposable
    {
        void OpenFile(string fileName, PathType pathType);
        EventRecord ReadEvent();
    }
}
