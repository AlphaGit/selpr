using System.Diagnostics.Eventing.Reader;

namespace SELPR
{
    public class EventLogFileFileReader: IEventLogFileReader
    {
        private EventLogReader _eventLogReader;
        public void OpenFile(string fileName, PathType pathType)
        {
            _eventLogReader = new EventLogReader(fileName, pathType);
        }

        public EventRecord ReadEvent()
        {
            return _eventLogReader.ReadEvent();
        }

        public void Dispose()
        {
            _eventLogReader?.Dispose();
        }
    }
}
