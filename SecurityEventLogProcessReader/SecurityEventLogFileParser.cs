using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SELPR
{
    public class SecurityEventLogFileParser : ISecurityEventLogFileParser
    {
        private readonly IEventLogFileReader _eventLogFileReader;

        private const string EventSchema = "http://schemas.microsoft.com/win/2004/08/events/event";

        public SecurityEventLogFileParser(IEventLogFileReader eventLogFileReader)
        {
            _eventLogFileReader = eventLogFileReader;
        }

        public List<SecurityEventLogEntry> OpenEventLogFile(string fileName)
        {
            var logEntries = new List<SecurityEventLogEntry>();
            _eventLogFileReader.OpenFile(fileName, PathType.FilePath);

            EventRecord record;
            while ((record = _eventLogFileReader.ReadEvent()) != null)
            {
                using (record)
                {
                    if (record.Task != 13312) continue; //TODO move to constant: TaskCreated

                    try
                    {
                        var xmlDoc = XDocument.Parse(record.ToXml());
                        logEntries.Add(new SecurityEventLogEntry()
                        {
                            TimeCreated = record.TimeCreated.GetValueOrDefault(DateTime.MinValue),
                            NewProcessId = Convert.ToInt32(GetEventDataValueFromXDoc(xmlDoc, "NewProcessId"), 16),
                            ParentProcessId = Convert.ToInt32(GetEventDataValueFromXDoc(xmlDoc, "ProcessId"), 16),
                            NewProcessName = GetEventDataValueFromXDoc(xmlDoc, "NewProcessName"),
                            CommandLine = GetEventDataValueFromXDoc(xmlDoc, "CommandLine")
                        });
                    }
                    catch (XmlException xe)
                    {
                        Console.WriteLine(xe.Message);
                    }
                }
            }

            return logEntries;
        }

        private string GetEventDataValueFromXDoc(XDocument root, string eventDataName)
        {
            var dataElement = XName.Get("Data", EventSchema);
            var nameAttribute = XName.Get("Name");

            return root.Descendants(dataElement)
                .First(d => d.Attributes(nameAttribute)
                    .Any(na => na.Value == eventDataName)
                ).Value;
        }
    }
}
